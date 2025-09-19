using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeShareApplication.ModelViews;

namespace RecipeShareApplication.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RecipesApi");
        }

        [BindProperty]
        public RecipeModelView Recipe { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _httpClient.PostAsJsonAsync("api/recipes", Recipe);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Failed to create recipe.");
            return Page();
        }
    }
}
