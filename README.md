# Recipe API

Small ASP.NET Web API for managing recipes. Projects:
- recipe.api — ASP.NET Core API (startup project)
- recipe.core — core models, DTOs and service interfaces/implementation
- recipe.data — EF Core DbContext and repository implementation
- recipe.tests — unit tests

## Prerequisites
- .NET 10 SDK installed (preview channel as required)
- dotnet-ef tool (for migrations) — optional but recommended:
  - `dotnet tool install --global dotnet-ef`
- SQLite (optional; database file is created by EF)
- Visual Studio 2022 or VS Code (note: older VS versions may warn about .NET 10 support)

## Quick CLI: build and run
1. Open a terminal at the repository root (where the solution is).
2. Restore and build:
   ```bash
   dotnet restore
   dotnet build
   ```
3. (Optional) Create and apply EF Core migrations (recommended for a fresh DB):
   ```bash
   # create migration in recipe.data project
   dotnet ef migrations add InitialCreate --project recipe.data --startup-project recipe.api --output-dir Migrations
   # apply to DB
   dotnet ef database update --project recipe.data --startup-project recipe.api
   ```
   If you prefer the Package Manager Console in Visual Studio, run:
   ```powershell
   # inside Visual Studio's __Package Manager Console__:
   Add-Migration InitialCreate -Project recipe.data -StartupProject recipe.api -OutputDir Migrations
   Update-Database -Project recipe.data -StartupProject recipe.api
   ```
4. Run the API:
   ```bash
   cd recipe.api
   dotnet run
   ```
   The app will print the listening URLs (e.g., https://localhost:5001). Use the printed URL for requests.

## Run in Visual Studio
1. Open the solution in Visual Studio.
2. Right-click the solution and choose __Restore NuGet Packages__ if needed.
3. Set `recipe.api` as the startup project.
4. Use __Build Solution__.
5. Start with __Debug > Start Debugging__ or __Debug > Start Without Debugging__.

## API Endpoints
- GET    /api/recipes                — list all recipes
- GET    /api/recipes/{id}        — get recipe by id
- GET    /api/recipes/search?q=term — search by name/description
- GET    /api/recipes/difficulty/{level} — filter by difficulty (Easy/Medium/Hard)
- POST   /api/recipes                — create (body: CreateRecipeDto JSON)
- PUT    /api/recipes/{id}           — update (body: CreateRecipeDto JSON)
- DELETE /api/recipes/{id}        — delete

### Example curl
```bash
curl -X GET https://localhost:5001/api/recipes -k
curl -X POST https://localhost:5001/api/recipes -H "Content-Type: application/json" -d '{"name":"Test","description":"d","prepTimeMinutes":5,"cookTimeMinutes":5,"servings":1,"ingredients":[{"id":1,"name":"Salt","quantity":1,"unit":"tsp"}],"instructions":["Do it"]}' -k
```
(-k for local dev self-signed certs; remove in production)

## Run tests
```bash
dotnet test
```

## Troubleshooting
- If you see a Visual Studio note similar to:
  > Targeting .NET 10.0 or higher in Visual Studio 2022 ... is not supported
  either update Visual Studio to the latest release that supports .NET 10 or use the CLI (`dotnet build` / `dotnet run`) with a matching .NET 10 SDK.
- If EF Core cannot find migrations, create them using the commands above and ensure `recipe.data` and `recipe.api` are referenced properly in the migration command.
- If you encounter circular reference build errors, ensure project references are only one-way: data depends on core (models/interfaces), api depends on core and data, core should not reference data.

## Notes
- The project uses a generic repository and EF Core with SQLite. Adjust connection string in `recipe.api/Program.cs` if you want a different database file/location.
- If you want me to add a startup step that calls `Database.Migrate()` automatically on app start, I can update `Program.cs` to do that.
