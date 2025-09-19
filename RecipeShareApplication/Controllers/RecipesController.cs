using _Data_Layer.Models;
using _Service_Layer.IRecipe_Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeShareApplication.ModelViews;

namespace RecipeShareApplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetAll([FromQuery] string? tag, [FromQuery] int? maxTime, [FromQuery] string? search)
        {
            if (!string.IsNullOrWhiteSpace(tag))
                return Ok(_recipeService.GetRecipesByTag(tag));

            if (!string.IsNullOrWhiteSpace(search))
                return Ok(_recipeService.SearchRecipes(search, maxTime));

            return Ok(_recipeService.GetAllRecipes());
        }

        // GET: api/recipes/5
        [HttpGet("{id:int}")]
        public ActionResult<Recipe> Get(int id)
        {
            var recipe = _recipeService.GetRecipe(id);
            if (recipe == null)
                return NotFound();

            return Ok(recipe);
        }

        [HttpPost]
        public ActionResult<Recipe> Create([FromBody] RecipeModelView recipeView)
        {

            var recipe = new Recipe
            {
                Id = recipeView.Id,
                Title = recipeView.Title,
                Ingredients = recipeView.Ingredients,
                Steps = recipeView.Steps,
                CookingTime = recipeView.CookingTime,
                DietaryTagsString = recipeView.DietaryTagsString
            };
            _recipeService.CreateRecipe(recipe);
            return CreatedAtAction(nameof(Get), new { id = recipe.Id }, recipe);
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] RecipeModelView recipeView)
        {
            var existing = _recipeService.GetRecipe(id);
            if (existing == null)
                return NotFound();

            existing.Title = recipeView.Title;
            existing.Ingredients = recipeView.Ingredients;
            existing.Steps = recipeView.Steps;
            existing.CookingTime = recipeView.CookingTime;
            existing.DietaryTagsString = recipeView.DietaryTagsString;

            _recipeService.UpdateRecipe(id, existing);

            return NoContent();
        }


        // DELETE: api/recipes/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var existing = _recipeService.GetRecipe(id);
            if (existing == null)
                return NotFound();

            _recipeService.RemoveRecipe(id);
            return NoContent();
        }
    }
}
