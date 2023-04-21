using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;
using System.Linq.Expressions;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.extensions;

internal static class QueryableExtensions
{
    internal static IQueryable<T> ApplyIncludes<T, U>(this IQueryable<T> queryable, params string[] includes)
        where T : BaseEntity<U>
    {
        foreach (var include in includes)
            queryable.Include(include);

        return queryable;
    }

    internal static IQueryable<T> ApplyPredict<T, U>(this IQueryable<T> queryable, Expression<Func<T, bool>> predicts)
        where T : BaseEntity<U>
        => queryable.Where(predicts);

    internal static IQueryable<T> ApplyOrderAsc<T, U>(this IQueryable<T> queryable, Expression<Func<T, object>> selector)
        where T : BaseEntity<U>
        => queryable.OrderBy(selector);

    internal static IQueryable<T> ApplyOrderDesc<T, U>(this IQueryable<T> queryable, Expression<Func<T, object>> selector)
        where T : BaseEntity<U>
        => queryable.OrderByDescending(selector);

    internal static IQueryable<T> ApplyPaging<T, U>(this IQueryable<T> queryable, int pageSize, int page)
        where T : BaseEntity<U>
        => queryable.Skip((page - 1) * page).Take(pageSize);
}
