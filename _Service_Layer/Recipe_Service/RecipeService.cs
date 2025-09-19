using _Data_Layer.Models;
using _Repository_Layer.IRecipe_Repo;
using _Service_Layer.IRecipe_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Service_Layer.Recipe_Service
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepo _recipeRepo;

        public RecipeService(IRecipeRepo recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }

        public Recipe GetRecipe(int id)
        {
            return _recipeRepo.GetRecipe(id);
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            
            return _recipeRepo.GetAllRecipes();
        }

        public void CreateRecipe(Recipe recipe)
        {
            _recipeRepo.CreateRecipe(recipe);
        }

        public void UpdateRecipe(int id, Recipe recipe)
        {
            _recipeRepo.UpdateRecipe(id, recipe);
        }

        public void RemoveRecipe(int id)
        {
            _recipeRepo.RemoveRecipe(id);
        }

        public IEnumerable<Recipe> GetRecipesByTag(string tag)
        {
            var all = GetAllRecipes();
            return all.Where(r =>
                !string.IsNullOrEmpty(r.DietaryTagsString) &&
                r.DietaryTagsString
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Any(t => t.Trim().Equals(tag, StringComparison.OrdinalIgnoreCase))
            );
        }

        public IEnumerable<Recipe> SearchRecipes(string query, int? maxCookingTime = null)
        {
            var all = GetAllRecipes();

            var filtered = all.Where(r =>
                r.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                r.Ingredients.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                r.Steps.Contains(query, StringComparison.OrdinalIgnoreCase));

            if (maxCookingTime.HasValue)
            {
                filtered = filtered
              .Where(r => int.TryParse(r.CookingTime, out var ct) && ct <= maxCookingTime.Value);
            }

            return filtered;
        }
    }
}
