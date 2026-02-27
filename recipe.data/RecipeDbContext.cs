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
  }
}