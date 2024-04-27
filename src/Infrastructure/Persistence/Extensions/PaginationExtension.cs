namespace Infrastructure.Persistence.Extensions;

public record Pagination
{
    private const int MaxPageSize = 20;
    private const int MinPageNumber = 1;
    
    public Pagination()
    {
        PageNumber = MinPageNumber;
        PageSize = MaxPageSize;
    }
    
    public Pagination(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < MinPageNumber ? MinPageNumber : pageNumber;
        PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
    }
    
    public int PageNumber { get; }
    public int PageSize { get; }
}


internal static class PaginationExtensions
{
    internal static IQueryable<T> Paged<T>(this IQueryable<T> queryable, Pagination pagination)
    {
        var skip = (pagination.PageNumber - 1) * pagination.PageSize;
        return queryable
            .Skip(skip)
            .Take(pagination.PageSize);
    }
}