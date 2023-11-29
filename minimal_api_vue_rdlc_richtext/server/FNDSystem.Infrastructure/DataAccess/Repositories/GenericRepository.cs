using FNDSystem.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    internal DBMasterContext masterDBContext; // 書き込みエンドポイント
    internal DBSlaveContext slaveDBContext; // 読み取りエンドポイント

    internal DbSet<TEntity> dbSetMaster;
    internal DbSet<TEntity> dbSetSlave;

    public GenericRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext)
    {
        this.masterDBContext = masterDBContext;
        dbSetMaster = masterDBContext.Set<TEntity>();

        this.slaveDBContext = slaveDBContext;
        dbSetSlave = slaveDBContext.Set<TEntity>();
    }

    /// <summary>
    /// データ追加
    /// </summary>
    /// <param name="entity"></param>
    public void Add(TEntity entity) => masterDBContext.Add(entity);

    /// <summary>
    /// データ範囲追加
    /// </summary>
    /// <param name="entities"></param>
    public void AddRange(IEnumerable<TEntity> entities) => masterDBContext.AddRange(entities);

    /// <summary>
    /// データ更新
    /// </summary>
    /// <param name="entity"></param>
    public void Update(TEntity entity)
    {
        SetLastUpdate(entity);
        masterDBContext.Update(entity);
    }

    /// <summary>
    /// LastUpdate 列のデフォルト値 (現在の日付と時刻) を設定します。
    /// </summary>
    /// <param name="entity"></param>
    private void SetLastUpdate(TEntity entity)
    {
        PropertyInfo? propertyInfo = entity.GetType().GetProperty("LastUpdate");
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(entity, DateTime.Now);
        }
    }

    /// <summary>
    /// LastUpdate 列のデフォルト値 (現在の日付と時刻) を設定します。
    /// </summary>
    /// <param name="entity"></param>
    private void SetLastUpdateRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            this.SetLastUpdate(entity);
        }
    }

    /// <summary>
    /// データ範囲更新
    /// </summary>
    /// <param name="entities"></param>
    public void UpdateRange(IEnumerable<TEntity> entities) 
    {
        SetLastUpdateRange(entities);
        masterDBContext.UpdateRange(entities);
    }

    /// <summary>
    /// データ削除
    /// </summary>
    /// <param name="entity"></param>
    public void Remove(TEntity entity) => masterDBContext.Remove(entity);

    /// <summary>
    /// データ範囲削除
    /// </summary>
    /// <param name="entities"></param>
    public void RemoveRange(IEnumerable<TEntity> entities) => masterDBContext.RemoveRange(entities);

    /// <summary>
    /// データ取得
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public TEntity? Get(Expression<Func<TEntity, bool>> expression) => dbSetSlave.Where(expression).FirstOrDefault();

    /// <summary>
    /// 全データ取得
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="PageNo"></param>
    /// <param name="Includes"></param>
    /// <returns></returns>
    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null, int PageNo = 0, string[]? Includes = null)
    {
        IQueryable<TEntity> query = dbSetSlave.AsQueryable();
        int PAGE_SIZE = 50;

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

    /// <summary>
    /// データ件数をカウントする
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public int Count(Expression<Func<TEntity, bool>>? expression = null)
    {
        IQueryable<TEntity> query = dbSetSlave.AsQueryable();
        if (expression is not null)
        {
            query = query.Where(expression);
        }

        return query.Count();
    }

    /// <summary>
    /// Max By 条件を取得する
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public int MaxBy(Expression<Func<TEntity, int?>> maxExpression, Expression<Func<TEntity, bool>>? whereExpression = null)
    {
        IQueryable<TEntity> query = dbSetSlave.AsQueryable();
        if(whereExpression != null)
        {
            query = query.Where(whereExpression);
        }
        return query.Max(maxExpression) ?? 0;
    }

    /// <summary>
    /// データをtruncateする
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool Truncate(TEntity entity)
    {
        string tableName = this.GetTableName();

        if (string.IsNullOrEmpty(tableName))
        {
            return false;
        }

        var result = this.masterDBContext?.Database.ExecuteSqlRaw($"TRUNCATE TABLE {tableName}");

        return (result > 0);
    }

    /// <summary>
    /// データ削除
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool DeleteAll(TEntity entity)
    {
        string tableName = this.GetTableName();

        if (string.IsNullOrEmpty(tableName))
        {
            return false;
        }

        var result = this.masterDBContext?.Database.ExecuteSqlRaw($"DELETE FROM {tableName}");

        return (result > 0);
    }

    /// <summary>
    /// 条件による削除
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public int DeleteBy(Expression<Func<TEntity, bool>>? whereExpression = null)
    {
        IQueryable<TEntity> query = dbSetMaster.AsQueryable();
        if (whereExpression != null)
        {
            query = query.Where(whereExpression);
        }
        return query.ExecuteDelete();
    }
    /// <summary>
    /// テーブル名取得
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private string GetTableName()
    {
        var dbContext = masterDBContext;
        var model = dbContext.Model;
        var entityTypes = model.GetEntityTypes();
        var entityType = entityTypes.First(t => t.ClrType == typeof(TEntity));
        var tableNameAnnotation = entityType.GetAnnotation("Relational:TableName");
        var tableName = tableNameAnnotation?.Value?.ToString();

        return tableName ?? string.Empty;
    }

}