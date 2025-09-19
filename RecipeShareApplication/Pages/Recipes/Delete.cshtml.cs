using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeShareApplication.ModelViews;

namespace RecipeShareApplication.Pages.Recipes
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RecipesApi");
        }

        [BindProperty]
        public RecipeModelView Recipe { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var recipe = await _httpClient.GetFromJsonAsync<RecipeModelView>($"api/recipes/{id}");
            if (recipe == null)
            {
                return NotFound();
            }

            Recipe = recipe;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/recipes/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Failed to delete recipe.");
            return Page();
        }
    }
}
