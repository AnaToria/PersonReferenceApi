namespace Infrastructure.Persistence.Extensions;

internal static class PaginationExtensions
{
    private const int MaxPageSize = 20;
    private const int MinPageNumber = 1;

    internal static IQueryable<T> Paged<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
    {
        var localPageNumber = pageNumber < MinPageNumber ? MinPageNumber : pageNumber;
        var localPageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
        
        var skip = (localPageNumber - 1) * localPageSize;
        return queryable
            .Skip(skip)
            .Take(localPageSize);
    }
}