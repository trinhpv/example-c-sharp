using System;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Helpers
{
    public static class PaginationHelper
    {
        public static IQueryable<T> ApplyPagination<T>(IQueryable<T> source, Pagination pagination)
        {
            var sortDirection = pagination.SortDirection == SortDirectionEnum.Ascending ? "OrderBy" : "OrderByDescending";
            var orderBy = pagination.SortBy ?? pagination.DefaultSortBy;

            return source.OrderBy(orderBy, sortDirection).Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, string sortDirection, params object[] values)
        {
            var type = typeof(T);
            var property = type.GetProperty(ordering);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var resultExp = Expression.Call(typeof(Queryable), sortDirection, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}