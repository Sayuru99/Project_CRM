namespace MyApp.SharedDomain.Queries
{
    public class PaginateQueryResponseBase<TResponse>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        //public int TotalPages { get; set; }
        public IEnumerable<TResponse> Items { get; set; }
        public int Count => Items.ToList().Count;

        public PaginateQueryResponseBase(IEnumerable<TResponse> items)
        {
            Items = items;
        }
    }
}
