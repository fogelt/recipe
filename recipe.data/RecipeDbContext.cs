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

    modelBuilder.Entity<Recipe>()
            .HasMany(r => r.Ingredients)
            .WithOne()
            .HasForeignKey("RecipeId")
            .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Recipe>()
        .Property(e => e.Instructions)
        .HasConversion(
            v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions)null!),
            v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, (System.Text.Json.JsonSerializerOptions)null!)
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