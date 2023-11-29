using System.Linq.Expressions;
using System.Reflection;
using Core.Models;

namespace Infrastructure.Services;

public abstract class GeneticService
{
    protected AuthenticatedUser? authenticatedUser;

    public Expression<Func<TEntity, bool>>? GetWhereExpression<TEntity, DTO>(DTO dto, bool isByTenant = true) where DTO : class
    {
        BinaryExpression? whereExpression = null;
        var entity = Expression.Parameter(typeof(TEntity));

        // add filter by Tenant Code
        if (isByTenant && authenticatedUser is not null)
        {
            var entityExpression = Expression.PropertyOrField(entity, "TenantCode");
            var constantExpression = Expression.Constant(authenticatedUser.TenantCode);
            var fieldExpression = Expression.Equal(entityExpression, constantExpression);
            whereExpression = (whereExpression != null) ? Expression.And(whereExpression, fieldExpression) : fieldExpression;
        }

        // entity props
        var props = typeof(TEntity).GetProperties();

        foreach (PropertyInfo prop in dto.GetType().GetProperties())
        {
            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            if (prop.GetValue(dto, null) != null && props.Where(pe => pe.Name == prop.Name).Count() > 0)
                if (type == typeof(string))
                {
                    var entityExpression = Expression.PropertyOrField(entity, prop.Name);
                    var constantExpression = Expression.Constant(prop.GetValue(dto, null));
                    var fieldExpression = Expression.Equal(entityExpression, constantExpression);
                    whereExpression = (whereExpression != null) ? Expression.And(whereExpression, fieldExpression) : fieldExpression;
                }

            // if (type == typeof(int))
            // if (type == typeof(DateTime))
        }

        if (whereExpression == null)
        {
            return null;
        }

        return Expression.Lambda<Func<TEntity, bool>>(whereExpression, entity);
    }
}