namespace recipe.core.Interfaces;

public interface IGenericRepository<T> where T : class
{
  Task<IEnumerable<T>> GetAllAsync();
  Task<T?> GetByIdAsync(int id);
  Task<T> CreateAsync(T entity);
  Task<IEnumerable<T>> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}