using recipe.core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace recipe.data.repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
  protected readonly RecipeDbContext _context;
  protected readonly DbSet<T> _dbSet;

  public GenericRepository(MyDbContext context)
  {
    _context = context;
    _dbSet = context.Set<T>();
  }

  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await _dbSet.ToListAsync();
  }

  public async Task<T?> GetByIdAsync(int id)
  {
    // Finds the entity by its primary key; returns null if not found
    return await _dbSet.FindAsync(id);
  }

  public async Task<T> CreateAsync(T entity)
  {
    await _dbSet.AddAsync(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
  {
    return await _dbSet.Where(predicate).ToListAsync();
  }
}