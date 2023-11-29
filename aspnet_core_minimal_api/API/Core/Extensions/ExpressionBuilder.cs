using System.Linq.Expressions;

namespace Core.Extensions
{
    public static class ExpressionBuilder
    {
        public static Expression<Func<TEntity, bool>> AndEx<TEntity>(this Expression<Func<TEntity, bool>> leftExpression, Expression<Func<TEntity, bool>> rightExpression) where TEntity : class
        {

            if (leftExpression is not null)
            {
                var paramExpr = Expression.Parameter(typeof(TEntity));
                var exprBody = Expression.And(leftExpression.Body, rightExpression.Body);
                exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
                var finalExpr = Expression.Lambda<Func<TEntity, bool>>(exprBody, paramExpr);
                return finalExpr;
            }

            return rightExpression;
        }
    }
}

internal class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;

    protected override Expression VisitParameter(ParameterExpression node)
        => base.VisitParameter(_parameter);

    internal ParameterReplacer(ParameterExpression parameter)
    {
        _parameter = parameter;
    }
}