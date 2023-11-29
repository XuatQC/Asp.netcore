namespace FNDSystem.Core.Helpers;
public static class ObjectUtils
{
    public static T NullToDefault<T>(T @this)
    {
        var type = typeof(T);
        foreach(var propertyInfo in type.GetProperties())
        {
            if (propertyInfo.PropertyType == typeof(String)) {
                if (propertyInfo.GetValue(@this) == null) {
                    propertyInfo.SetValue(@this, string.Empty);
                }
            } else if (propertyInfo.PropertyType == typeof(bool?)) {
                if (propertyInfo.GetValue(@this) == null) {
                    propertyInfo.SetValue(@this, false);
                }
            }
        }

        return @this;
    }

    /// <summary>
    /// インデックス付きのforeachの使用 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <remarks>foreach (var (item, index) in collection.WithIndex())</remarks>
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
    {
        return source.Select((item, index) => (item, index));
    }
}