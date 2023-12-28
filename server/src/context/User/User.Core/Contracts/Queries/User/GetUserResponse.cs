using MyApp.SharedDomain.Queries;
using User.Core.Contracts.Queries.User.Image;

namespace User.Core.Contracts.Queries
{
    public class GetUserResponse : QueryResponseBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public virtual GetUserImageResponse? Image { get; set; }
    }
}
