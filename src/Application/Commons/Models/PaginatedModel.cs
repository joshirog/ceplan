namespace Application.Commons.Models;

public class Paginated<T>
{
    public List<T> Items { get; set; } = new();
    public int PageIndex { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }

    public Paginated()
    {
        
    }

    public Paginated(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    public static async Task<Paginated<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await Task.Run(() => source.Count());
        var items = await Task.Run(() => source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());

        return new Paginated<T>(items, count, pageIndex, pageSize);
    }
}