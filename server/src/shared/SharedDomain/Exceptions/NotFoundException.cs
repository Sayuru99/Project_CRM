using System.Net;

namespace MyApp.SharedDomain.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public NotFoundException(object item) : base(BuildMessage(item), HttpStatusCode.NoContent)
        {
        }

        private static string BuildMessage(object item)
        {
            return $"The item {item} was not found.";
        }
    }
}
