using System.Linq.Expressions;

namespace FNDSystem.Core.Domain.RepositoryContracts;
public interface IGenericRepository<TEntity> where TEntity : class
{
    TEntity? Get(Expression<Func<TEntity, bool>> expression);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null, int PageNo = 0, string[]? Includes = null);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    bool Truncate(TEntity entity);
    bool DeleteAll(TEntity entity);
    int DeleteBy(Expression<Func<TEntity, bool>>? whereExpression = null);
    int Count(Expression<Func<TEntity, bool>>? expression = null);
    int MaxBy(Expression<Func<TEntity, int?>> maxExpression, Expression<Func<TEntity, bool>>? whereExpression = null);
    // Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    // Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    // Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    // Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    // Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}
