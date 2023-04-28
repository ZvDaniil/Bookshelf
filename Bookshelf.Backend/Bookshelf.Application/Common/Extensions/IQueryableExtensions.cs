using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Application.Common.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<TEntity> IgnoreQueryFilters<TEntity>(this IQueryable<TEntity> query, bool ignore)
        where TEntity : class
    {
        return ignore ? query.IgnoreQueryFilters() : query;
    }
}