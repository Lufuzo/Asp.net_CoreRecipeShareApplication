using _Data_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace _Repository_Layer.IRecipe_Repo
{
    public interface IRecipeRepo
    {
        Recipe GetRecipe(int id);
        IEnumerable<Recipe> GetAllRecipes();
        void UpdateRecipe(int id, Recipe account);
        void CreateRecipe(Recipe record);
        void RemoveRecipe(int id);
    }
}
