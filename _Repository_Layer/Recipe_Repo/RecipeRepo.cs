using _Data_Layer.Models;
using _Repository_Layer.IRecipe_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Repository_Layer.Recipe_Repo
{
    public class RecipeRepo : IRecipeRepo
    {
        private readonly ApplicationDbContext _db;

        public RecipeRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public Recipe GetRecipe(int id)
        {
            return _db.Recipes.FirstOrDefault(r => r.Id == id);
        }
        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _db.Recipes.ToList();
        }
        public void UpdateRecipe(int id, Recipe recipe)
        {
            var existing = _db.Recipes.FirstOrDefault(r => r.Id == id);
            if (existing != null)
            {
                existing.Title = recipe.Title;
                existing.Ingredients = recipe.Ingredients;
                existing.Steps = recipe.Steps;
                existing.CookingTime = recipe.CookingTime;
                existing.DietaryTagsString = recipe.DietaryTagsString;
                //existing.DietaryTagsJson = System.Text.Json.JsonSerializer.Serialize(recipe.DietaryTags);

                _db.SaveChanges();
            }
        }
        public void CreateRecipe(Recipe record)
        {
            _db.Recipes.Add(record);
            _db.SaveChanges();
        }
        public void RemoveRecipe(int id)
        {
            var recipe = _db.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipe != null)
            {
                _db.Recipes.Remove(recipe);
                _db.SaveChanges();
            }
        }
    }
}
