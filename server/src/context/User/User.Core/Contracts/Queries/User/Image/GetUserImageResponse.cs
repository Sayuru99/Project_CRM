using MyApp.SharedDomain.Queries;

namespace User.Core.Contracts.Queries.User.Image
{
    public class GetUserImageResponse : QueryResponseBase
    {
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
