using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeShareApplication.ModelViews;

namespace RecipeShareApplication.Pages.Recipes
{
    public class IndexModel : PageModel
    {
       

    private readonly HttpClient _httpClient;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RecipesApi");
        }

        public List<RecipeModelView> Recipes { get; set; } = new();

        public async Task OnGetAsync(string? tag, string? search, int? maxTime)
        {
            string query = "api/recipes";
            if (!string.IsNullOrEmpty(tag))
                query += $"?tag={tag}";
            else if (!string.IsNullOrEmpty(search))
                query += $"?search={search}&maxTime={maxTime}";

            var response = await _httpClient.GetFromJsonAsync<List<RecipeModelView>>(query);
            if (response != null)
                Recipes = response;
        }
    }
}
