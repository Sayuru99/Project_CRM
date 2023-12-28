using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace SharedDomain.Extensions
{
    public static class RequestExtensions
    {
        public static string GetRequestIP(this HttpRequest request)
        {
            string ip = string.Empty;

            if (request.Headers.ContainsKey("X-Forwarded-For"))
                ip = request.Headers["X-Forwarded-For"];

            if (string.IsNullOrWhiteSpace(ip) && request.HttpContext?.Connection?.RemoteIpAddress != null)
                ip = request.HttpContext.Connection.RemoteIpAddress.ToString();

            if (string.IsNullOrWhiteSpace(ip))
                ip = request.GetHeaderValueAs<string>("REMOTE_ADDR");

            if (string.IsNullOrWhiteSpace(ip))
                ip = "0.0.0.0";

            return ip;
        }

        private static T GetHeaderValueAs<T>(this HttpRequest request, string headerName)
        {
            StringValues values = StringValues.Empty;

            if (request.HttpContext?.Request?.Headers?.TryGetValue(headerName, out values) ?? false)
            {
                string rawValues = values.ToString();

                if (!string.IsNullOrWhiteSpace(rawValues))
                {
                    return (T)Convert.ChangeType(values.ToString(), typeof(T));
                }
            }

            return default;
        }
    }
}