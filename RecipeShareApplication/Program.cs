using _Data_Layer.Models;
using _Repository_Layer.IRecipe_Repo;
using _Repository_Layer.Recipe_Repo;
using _Service_Layer.IRecipe_Service;
using _Service_Layer.Recipe_Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repo + service
builder.Services.AddScoped<IRecipeRepo, RecipeRepo>();
builder.Services.AddScoped<IRecipeService, RecipeService>();

builder.Services.AddHttpClient("RecipesApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7000/"); // API URL
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();