using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repositories;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    internal DBMasterContext masterDBContext; // Write endpoint
    internal DBSlaveContext slaveDBContext; // Read endpoint

    internal DbSet<TEntity> dbSetMaster;
    internal DbSet<TEntity> dbSetSlave;

    public GenericRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext)
    {
        this.masterDBContext = masterDBContext;
        this.dbSetMaster = masterDBContext.Set<TEntity>();

        this.slaveDBContext = slaveDBContext;
        this.dbSetSlave = slaveDBContext.Set<TEntity>();
    }

    public void Add(TEntity entity) => this.masterDBContext.Add(entity);

    public void AddRange(IEnumerable<TEntity> entities) => this.masterDBContext.AddRange(entities);


    public void Update(TEntity entity) => this.masterDBContext.Update(entity);

    public void UpdateRange(IEnumerable<TEntity> entities) => this.masterDBContext.UpdateRange(entities);

    public void Remove(TEntity entity) => this.masterDBContext.Remove(entity);

    public void RemoveRange(IEnumerable<TEntity> entities) => this.masterDBContext.RemoveRange(entities);

    public TEntity Get(Expression<Func<TEntity, bool>> expression) => this.dbSetSlave.Where(expression).FirstOrDefault();

    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression, int PageNo = 0, string[]? Includes = null)
    {
        int PAGE_SIZE = 50;
        IQueryable<TEntity> query = this.dbSetSlave.AsQueryable();
        if (expression is not null)
        {
            query = query.Where(expression);
        }

        foreach (string relation in Includes ?? new string[] { })
        {
            query = query.Include(relation);
        }

        if (PageNo > 0)
        {
            query = query.Skip(PAGE_SIZE * (PageNo - 1)).Take(PAGE_SIZE);
        }

        return query.AsEnumerable();
    }

    public int Count(Expression<Func<TEntity, bool>>? expression)
    {
        IQueryable<TEntity> query = this.dbSetSlave.AsQueryable();
        if (expression is not null)
        {
            query = query.Where(expression);
        }

        return query.Count();
    }

    // public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) => await this.dbSetSlave.ToListAsync(cancellationToken);

    // public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    // {
    //     if (expression is not null)
    //     {
    //         return await this.dbSetSlave.Where(expression).ToListAsync(cancellationToken);
    //     }
    //     else
    //     {
    //         return await this.dbSetSlave.ToListAsync(cancellationToken);
    //     }
    // }

    // public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    // {
    //     if (expression is not null)
    //     {
    //         return await this.dbSetSlave.FirstOrDefaultAsync(expression);
    //     }
    //     else
    //     {
    //         return await this.dbSetSlave.FirstOrDefaultAsync();
    //     }
    // }

    // public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) => await this.masterDBContext.AddAsync(entity);
    // public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) => await this.masterDBContext.AddRangeAsync(entities);

}