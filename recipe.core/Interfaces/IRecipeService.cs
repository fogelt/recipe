using recipe.core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace recipe.core.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAllAsync();
        Task<Recipe?> GetByIdAsync(int id);
        Task<Recipe> CreateAsync(Recipe recipe);
        Task<Recipe?> UpdateAsync(int id, Recipe recipe);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Recipe>> SearchAsync(string term);
        Task<IEnumerable<Recipe>> GetByDifficultyAsync(string level);
    }
}