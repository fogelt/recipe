using Microsoft.EntityFrameworkCore;
using recipe.data;
using recipe.core.Interfaces;
using recipe.core.Services;
using recipe.data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RecipeDbContext>(options =>
    options.UseSqlite("Data Source=recipes.db"));

builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();