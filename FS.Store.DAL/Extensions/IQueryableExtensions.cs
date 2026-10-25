using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace FS.Store.DAL.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySortingDynamic<T>(this IQueryable<T> query, Dictionary<string, string> ordering,
            Dictionary<string, Expression<Func<T, object>>>? complexExpressions = null)
        {
            bool first = true;

            foreach (var param in ordering)
            {
                var property = param.Key;
                var direction = param.Value?.ToLowerInvariant();

                if (string.IsNullOrWhiteSpace(direction))
                    continue;

                if (complexExpressions != null && complexExpressions.TryGetValue(property, out var expr))
                {
                    if (first)
                    {
                        query = direction == "desc"
                            ? query.OrderByDescending(expr)
                            : query.OrderBy(expr);
                    }
                    else
                    {
                        query = direction == "desc"
                            ? ((IOrderedQueryable<T>)query).ThenByDescending(expr)
                            : ((IOrderedQueryable<T>)query).ThenBy(expr);
                    }
                }
                else
                {
                    if (direction == "asc")
                    {
                        query = first
                            ? query.OrderBy(property)
                            : ((IOrderedQueryable<T>)query).ThenBy(property);
                    }
                    else if (direction == "desc")
                    {
                        query = first
                            ? query.OrderBy($"{property} descending")
                            : ((IOrderedQueryable<T>)query).ThenBy($"{property} descending");
                    }
                }

                first = false;
            }

            return query;
        }
    }
}
