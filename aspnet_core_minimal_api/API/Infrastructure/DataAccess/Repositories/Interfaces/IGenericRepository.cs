using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Repositories;
public interface IGenericRepository<TEntity> where TEntity : class
{
    TEntity Get(Expression<Func<TEntity, bool>> expression);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null, int PageNo = 0, string[]? Includes = null);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    // Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    // Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    // Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    // Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    // Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}
