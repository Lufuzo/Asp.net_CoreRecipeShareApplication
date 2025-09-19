using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecipeShareApplication.ModelViews;
using System.Net.Http.Json;
using System.Text.Json;

public class EditModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<EditModel> _logger;

    public EditModel(IHttpClientFactory httpClientFactory, ILogger<EditModel> logger)
    {
        // Make sure the name here ("RecipesApi") matches Program.cs AddHttpClient name
        _httpClient = httpClientFactory.CreateClient("RecipesApi");
        _logger = logger;
    }

    // bind the editable Recipe viewmodel
    [BindProperty]
    public RecipeModelView Recipe { get; set; } = new();

    // bind a CSV string for tags for simpler form input
    [BindProperty]
    public string DietaryTagsCsv { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var resp = await _httpClient.GetAsync($"api/recipes/{id}");
            if (!resp.IsSuccessStatusCode)
            {
                _logger.LogWarning("GET api/recipes/{Id} returned {StatusCode}", id, resp.StatusCode);
                return NotFound();
            }

            var recipe = await resp.Content.ReadFromJsonAsync<RecipeModelView>();
            if (recipe == null) return NotFound();

            Recipe = recipe;

            // populate CSV tag field for the form (join with comma + space)
            DietaryTagsCsv = Recipe.DietaryTags != null ? string.Join(", ", Recipe.DietaryTags) : string.Empty;

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading recipe {Id}", id);
            return StatusCode(500, "Error loading recipe");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Convert CSV tags to List<string> and assign back to the Recipe model
        Recipe.DietaryTags = string.IsNullOrWhiteSpace(DietaryTagsCsv)
            ? new List<string>()
            : DietaryTagsCsv.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(t => t.Trim())
                            .Where(t => !string.IsNullOrWhiteSpace(t))
                            .ToList();

        try
        {
            // Call PUT api/recipes/{id}
            var response = await _httpClient.PutAsJsonAsync($"api/recipes/{Recipe.Id}", Recipe);

            if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                // redirect to the Index page in the same folder
                return RedirectToPage("./Index");
            }

            // read response body for debug
            var body = await response.Content.ReadAsStringAsync();
            _logger.LogWarning("PUT api/recipes/{Id} failed: {StatusCode} {Body}", Recipe.Id, response.StatusCode, body);

            ModelState.AddModelError(string.Empty, $"Update failed: {response.StatusCode} - {body}");
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating recipe {Id}", Recipe.Id);
            ModelState.AddModelError(string.Empty, "Unexpected error updating recipe.");
            return Page();
        }
    }
}
