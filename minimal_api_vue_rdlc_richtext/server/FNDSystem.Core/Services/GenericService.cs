using FNDSystem.Core.Dto;
using System.Linq.Expressions;
using System.Reflection;

namespace FNDSystem.Core.Services;

public abstract class GeneticService
{
    protected AuthenticatedUser? authenticatedUser;

    /// <summary>
    /// 条件式を取得する
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="DTO"></typeparam>
    /// <param name="dto"></param>
    /// <param name="isBoolNant"></param>
    /// <returns></returns>
    public Expression<Func<TEntity, bool>>? GetWhereExpression<TEntity, DTO>(DTO dto, bool isBoolNant = true) where DTO : class
    {
        BinaryExpression? whereExpression = null;
        var entity = Expression.Parameter(typeof(TEntity));

        // テナントコードによるフィルターを追加
        if (isBoolNant && authenticatedUser is not null)
        {
            var entityExpression = Expression.PropertyOrField(entity, "TenantCode");
            var constantExpression = Expression.Constant(authenticatedUser.TenantCode);
            var fieldExpression = Expression.Equal(entityExpression, constantExpression);
            whereExpression = whereExpression != null ? Expression.And(whereExpression, fieldExpression) : fieldExpression;
        }

        // エンティティの小道具
        var props = typeof(TEntity).GetProperties();

        foreach (PropertyInfo prop in dto.GetType().GetProperties())
        {
            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            if ((prop.GetValue(dto, null) != null && props?.Count(pe => pe.Name == prop.Name) > 0) && (type == typeof(string)))
            {
                var entityExpression = Expression.PropertyOrField(entity, prop.Name);
                var constantExpression = Expression.Constant(prop.GetValue(dto, null));
                var fieldExpression = Expression.Equal(entityExpression, constantExpression);
                whereExpression = whereExpression != null ? Expression.And(whereExpression, fieldExpression) : fieldExpression;
            }
        }

        if (whereExpression == null)
        {
            return null;
        }

        return Expression.Lambda<Func<TEntity, bool>>(whereExpression, entity);
    }
}