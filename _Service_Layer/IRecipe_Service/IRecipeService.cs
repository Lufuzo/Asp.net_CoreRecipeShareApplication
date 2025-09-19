using _Data_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Service_Layer.IRecipe_Service
{
    public interface IRecipeService
    {
        Recipe GetRecipe(int id);
        IEnumerable<Recipe> GetAllRecipes();
        void CreateRecipe(Recipe recipe);
        void UpdateRecipe(int id, Recipe recipe);
        void RemoveRecipe(int id);

        // Extra (useful for Task 1 filters):
        IEnumerable<Recipe> GetRecipesByTag(string tag);
        IEnumerable<Recipe> SearchRecipes(string query, int? maxCookingTime = null);


    }
}
