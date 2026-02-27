using recipe.core.Models;
using System.Linq.Expressions;

namespace recipe.core.Interfaces;

public interface IRecipeRepository
{
  Task<IEnumerable<Recipe>> GetAllAsync();
  Task<Recipe?> GetByIdAsync(int id);
  Task<Recipe> CreateAsync(Recipe recipe);
  Task<Recipe> UpdateAsync(Recipe recipe);
  Task<bool> DeleteAsync(int id);
  Task<IEnumerable<Recipe>> FindAsync(Expression<Func<Recipe, bool>> predicate);
}