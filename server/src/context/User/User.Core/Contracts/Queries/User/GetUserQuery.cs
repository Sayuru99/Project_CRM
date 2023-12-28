using MyApp.SharedDomain.Queries;

namespace User.Core.Contracts.Queries
{
    public class GetUserQuery : QueryBase<GetUserResponse>
    {
        public string Email { get; set; }
    }
}
