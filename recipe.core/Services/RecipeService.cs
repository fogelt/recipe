using recipe.core.DTOs;
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

        return await _repository.UpdateAsync(existing);
    }

    public async Task<bool> DeleteRecipeAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Recipe>> SearchRecipesAsync(string term)
    {

    }

    public async Task<IEnumerable<Recipe>> GetRecipesByDifficultyAsync(string level)
    {

    }
}