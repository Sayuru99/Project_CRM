namespace MyApp.SharedDomain.ValueObjects
{
    public class PaginationResponse<T> : ValueObject
        where T : class
    {
        private readonly Pagination _pagination;

        public int Page => _pagination.Page;
        public int PageSize => _pagination.Size;
        //public int TotalPages => (int)Math.Ceiling((double)Count / _pagination.Size);
        public IReadOnlyList<T> Items { get; }
        //public int Count => Items?.Count + _pagination.Skip ?? 0;
        public int Count => Items?.Count ?? 0;

        public PaginationResponse(IReadOnlyList<T> items, Pagination pagination)
        {
            _pagination = pagination;
            Items = items;
        }

        public override object GetValue()
        {
            return $"{typeof(T)}:{_pagination.Page}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return typeof(T);
            yield return _pagination.Page;
        }
    }
}
