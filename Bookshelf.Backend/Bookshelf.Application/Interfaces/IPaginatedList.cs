namespace Bookshelf.Application.Interfaces;

public interface IPaginatedList<T>
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }

    public IList<T> Items { get; }

    public bool HasNextPage { get; }
    public bool HasPreviousPage { get; }
}

//public class PaginatedList<T> : List<T>
//{
//    public int PageIndex { get; private set; }
//    public int PageSize { get; private set; }
//    public int TotalCount { get; private set; }
//    public int TotalPages { get; private set; }

//    public PaginatedList(IEnumerable<T> entities, int pageIndex, int pageSize)
//    {
//        PageIndex = pageIndex;
//        PageSize = pageSize;
//        TotalCount = entities.Count();
//        TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

//        this.AddRange(entities);
//    }

//    public bool HasPreviousPage => (PageIndex > 0);

//    public bool HasNextPage => (PageIndex + 1 < TotalPages);
//}