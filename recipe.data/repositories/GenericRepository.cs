using recipe.core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace recipe.data.repositories;

public class GenericRepository<T>(RecipeDbContext context) : IGenericRepository<T> where T : class
{
  protected readonly RecipeDbContext _context = context;
  protected readonly DbSet<T> _dbSet = context.Set<T>();

  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await _dbSet.ToListAsync();
  }

  public async Task<T?> GetByIdAsync(int id)
  {
    return await _dbSet.FindAsync(id);
  }

  public async Task<T> CreateAsync(T entity)
  {
    await _dbSet.AddAsync(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var found = await _dbSet.FindAsync(id);
        if (found == null) return false;
        _dbSet.Remove(found);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
  {
    return await _dbSet.Where(predicate).ToListAsync();
  }
}