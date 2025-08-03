using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Forum.Infrastructure.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> CustomInclude<T, TProperty>(this IQueryable<T> source, 
            Expression<Func<T, TProperty>> navigationProperty,
            bool isIncluded) where T : class where TProperty : class
        {
            if (isIncluded)
            {
                return source.Include(navigationProperty);
            }
            return source;
        }

        public static IQueryable<T> AllowQueryFilters<T>(this IQueryable<T> source, bool isAllowed)
            where T : class
        {
            return isAllowed ? source.IgnoreQueryFilters() : source;
        }
    }
}
