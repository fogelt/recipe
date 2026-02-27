using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using recipe.core.Models;

namespace recipe.data;

public class RecipeDbContext(DbContextOptions<RecipeDbContext> options) : DbContext(options)
{
  public DbSet<Recipe> Recipes { get; set; }
  public DbSet<Ingredient> Ingredients { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // This tells EF Core how to handle List<string> in SQLite
    modelBuilder.Entity<Recipe>()
        .Property(e => e.Instructions)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null!)
        );

    modelBuilder.Entity<Ingredient>().HasData(
        new Ingredient { Id = 1, Name = "Flour", Quantity = 500, Unit = "g" },
        new Ingredient { Id = 2, Name = "Tomato Sauce", Quantity = 200, Unit = "ml" },
        new Ingredient { Id = 3, Name = "Mozzarella", Quantity = 150, Unit = "g" }
    );

    modelBuilder.Entity<Recipe>().HasData(new Recipe
    {
      Id = 1,
      Name = "Classic Pancakes",
      Description = "Fluffy breakfast pancakes.",
      PrepTimeMinutes = 10,
      CookTimeMinutes = 15,
      Servings = 4,
      Difficulty = "Easy",
      CreatedAt = new DateTime(2024, 1, 1),
      Instructions = ["Mix dry ingredients", "Add milk and eggs", "Fry on pan"]
    });
  }
}