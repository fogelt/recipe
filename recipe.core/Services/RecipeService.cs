using recipe.core.DTOs;
using recipe.core.Interfaces;
using recipe.core.Models;

namespace recipe.core.Services;

public class RecipeService : IRecipeService
{
  private readonly IRecipeRepository _repository;

  public RecipeService(IRecipeRepository repository)
  {
    _repository = repository;
  }

  public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
  {
    return await _repository.GetAllAsync();
  }

  public async Task<Recipe?> GetRecipeByIdAsync(int id)
  {
    return await _repository.GetByIdAsync(id);
  }

  public async Task<Recipe> CreateRecipeAsync(CreateRecipeDto dto)
  {
    var recipe = new Recipe
    {
      Name = dto.Name,
      Description = dto.Description,
      PrepTimeMinutes = dto.PrepTimeMinutes,
      CookTimeMinutes = dto.CookTimeMinutes,
      Servings = dto.Servings,
      Instructions = dto.Instructions?.ToList() ?? new List<string>(),
      Ingredients = dto.Ingredients?.Select(i => new Ingredient
      {
        Id = i.Id,
        Name = i.Name,
        Quantity = i.Quantity,
        Unit = i.Unit
      }).ToList(),
      CreatedAt = DateTime.UtcNow,
      Difficulty = DetermineDifficulty(dto)
    };

    return await _repository.CreateAsync(recipe);
  }

  public async Task<Recipe?> UpdateRecipeAsync(int id, CreateRecipeDto dto)
  {
    var existing = await _repository.GetByIdAsync(id);
    if (existing == null) return null;

    existing.Name = dto.Name;
    existing.Description = dto.Description;
    existing.PrepTimeMinutes = dto.PrepTimeMinutes;
    existing.CookTimeMinutes = dto.CookTimeMinutes;
    existing.Servings = dto.Servings;
    existing.Instructions = dto.Instructions?.ToList() ?? new List<string>();
    existing.Ingredients = dto.Ingredients?.Select(i => new Ingredient
    {
      Id = i.Id,
      Name = i.Name,
      Quantity = i.Quantity,
      Unit = i.Unit
    }).ToList();
    existing.Difficulty = DetermineDifficulty(dto);

    return await _repository.UpdateAsync(existing);
  }

  public async Task<bool> DeleteRecipeAsync(int id)
  {
    return await _repository.DeleteAsync(id);
  }

  public async Task<IEnumerable<Recipe>> SearchRecipesAsync(string term)
  {
    if (string.IsNullOrWhiteSpace(term))
      return await _repository.GetAllAsync();

    var t = term.Trim().ToLower();
    return await _repository.FindAsync(r =>
      (r.Name != null && r.Name.ToLower().Contains(t)) ||
      (r.Description != null && r.Description.ToLower().Contains(t)));
  }

  public async Task<IEnumerable<Recipe>> GetRecipesByDifficultyAsync(string level)
  {
    if (string.IsNullOrWhiteSpace(level))
      return [];

    var lvl = level.Trim().ToLower();
    return await _repository.FindAsync(r => r.Difficulty != null && r.Difficulty.ToLower() == lvl);
  }

  private static string DetermineDifficulty(CreateRecipeDto dto)
  {
    var steps = dto.Instructions?.Count ?? 0;
    if (steps >= 8) return "Hard";
    if (steps >= 4) return "Medium";
    return "Easy";
  }
}