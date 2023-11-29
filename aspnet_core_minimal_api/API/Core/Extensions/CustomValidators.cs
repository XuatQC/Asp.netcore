using FluentValidation;

namespace Core.Extensions;
public static class CustomValidators
{
    public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainFewerThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int limit)
    {
        return ruleBuilder.Must(list => list.Count < limit).WithMessage($"The list contains too many items. Max {limit}");
    }

    public static IRuleBuilderOptions<T, string?> WithInValues<T>(this IRuleBuilder<T, string?> ruleBuilder, IList<string> values)
    {
        return ruleBuilder.Must(value => !string.IsNullOrEmpty(value) && values.Contains(value)).WithMessage($"Value is invalid. The possible value is {string.Join(",", values)}");
    }
}