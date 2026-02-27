using recipe.core.Interfaces;
using recipe.core.Models;

namespace recipe.core.services;

public class RecipeService : IRecipeService
{
  private readonly IGenericRepository<Recipe> _repository;

  public RecipeService(IGenericRepository<Recipe> repository)
  {
    _repository = repository;
  }

  public async Task<IEnumerable<Recipe>> GetAllAsync()
  {
    return await _repository.GetAllAsync();
  }

  public async Task<Recipe?> GetByIdAsync(int id)
  {
    return await _repository.GetByIdAsync(id);
  }

  public Task<Recipe> CreateAsync(Recipe recipe)
  {
    throw new NotImplementedException();
  }
  public Task<bool> DeleteAsync(int id)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Recipe>> GetByDifficultyAsync(string level)
  {
    throw new NotImplementedException();
  }
  public Task<IEnumerable<Recipe>> SearchAsync(string term)
  {
    throw new NotImplementedException();
  }
  public Task<Recipe?> UpdateAsync(int id, Recipe recipe)
  {
    throw new NotImplementedException();
  }
}