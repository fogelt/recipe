using Microsoft.EntityFrameworkCore;
using recipe.core.Interfaces;
using recipe.core.Models;
using System.Linq.Expressions;

namespace recipe.data.Repositories;

public class RecipeRepository(RecipeDbContext context) : IRecipeRepository
{
  private readonly RecipeDbContext _context = context;

  public async Task<IEnumerable<Recipe>> GetAllAsync()
  {
    return await _context.Recipes
        .Include(r => r.Ingredients)
        .ToListAsync();
  }

  public async Task<Recipe?> GetByIdAsync(int id)
  {
    return await _context.Recipes
        .Include(r => r.Ingredients)
        .FirstOrDefaultAsync(r => r.Id == id);
  }

  public async Task<Recipe> CreateAsync(Recipe recipe)
  {
    await _context.Recipes.AddAsync(recipe);
    await _context.SaveChangesAsync();
    return recipe;
  }

  public async Task<Recipe> UpdateAsync(Recipe recipe)
  {
    _context.Recipes.Update(recipe);
    await _context.SaveChangesAsync();
    return recipe;
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var recipe = await _context.Recipes.FindAsync(id);
    if (recipe == null) return false;

    _context.Recipes.Remove(recipe);
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<IEnumerable<Recipe>> FindAsync(Expression<Func<Recipe, bool>> predicate)
  {
    return await _context.Recipes
        .Include(r => r.Ingredients)
        .Where(predicate)
        .ToListAsync();
  }
}