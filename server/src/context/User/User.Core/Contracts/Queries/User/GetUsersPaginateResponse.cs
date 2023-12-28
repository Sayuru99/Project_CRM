using MyApp.SharedDomain.Queries;

namespace User.Core.Contracts.Queries
{
    public class GetUsersPaginateResponse : PaginateQueryResponseBase<GetUserResponse>
    {
        public GetUsersPaginateResponse(IEnumerable<GetUserResponse> items) : base(items)
        {
        }
    }
}
