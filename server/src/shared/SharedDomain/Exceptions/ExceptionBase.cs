using System.Net;

namespace MyApp.SharedDomain.Exceptions
{
    public class ExceptionBase : Exception
    {
        public int StatusCode { get; set; }

        public ExceptionBase(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = (int) statusCode;
        }
    }
}
