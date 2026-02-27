using recipe.core.DTOs;
using recipe.core.Models;

namespace recipe.core.Interfaces
{
  public interface IRecipeService
  {
    Task<IEnumerable<Recipe>> GetAllRecipesAsync();
    Task<Recipe?> GetRecipeByIdAsync(int id);
    Task<Recipe> CreateRecipeAsync(CreateRecipeDto dto);
    Task<Recipe?> UpdateRecipeAsync(int id, CreateRecipeDto dto);
    Task<bool> DeleteRecipeAsync(int id);
    Task<IEnumerable<Recipe>> SearchRecipesAsync(string term);
    Task<IEnumerable<Recipe>> GetRecipesByDifficultyAsync(string level);
  }
}