namespace BlazorApp.Shared.SeedWork
{
    public class PaginatedList<T> : PagedListBase
    {
        public List<T> Items { get; }

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            Items = items;
        }
    }
}