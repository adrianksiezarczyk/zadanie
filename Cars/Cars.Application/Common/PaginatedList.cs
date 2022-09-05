using Microsoft.EntityFrameworkCore;

namespace Cars.Application.Common;

public class PaginatedList
{
    public List<object> Data { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalItems { get; }

    public PaginatedList(List<object> items, int count, int pageIndex)
    {
        PageNumber = pageIndex;
        TotalItems = count;
        Data = items;
    }

    public static async Task<PaginatedList> Create(IQueryable<object> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        if (pageIndex <= 0) pageIndex = 1;
        if (pageSize <= 0) pageSize = 1;

        var items = await source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList(items, count, pageIndex);
    }
}