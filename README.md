# Asp.net_CoreRecipeShareApplication

> A small ASP.NET Core Web API Applicationwith REST API + lightweight Razor UI for managing recipes (Task 1 deliverable)
>
> ## Repository layout

```
/ (root)
  README.md                <-- this file
  SOLUTION.md              <-- architecture notes, trade-offs, costs, security
_Data_Layer
-Data/
        AppDbContext.cs
        SeedData.cs
_Repository_Layer
_Service_Layer
  src/
    RecipeShareApplication.Api/       <-- ASP.NET Core 8 (or 6) Web API project
      Controllers/
        RecipesController.cs      
      ViewmodelsModels/
        .cs
      Program.cs
      appsettings.json
  ui/
    recipe-share-ui/       <-- lightweight front-end (single-page Razor)
  .gitignore
  docker-compose.yml      <-- optional: SQL Server + API for local dev
  README_IMAGES/          <-- optional images (architecture diagram PNG)
```
