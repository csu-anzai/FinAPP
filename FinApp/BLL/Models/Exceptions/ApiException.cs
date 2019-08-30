using System.Net;

namespace BLL.Models.Exceptions
{
    public class ApiException : CustomExeption
    {
        public ApiException(HttpStatusCode statusCode) : base(statusCode) { }

        public ApiException(HttpStatusCode statusCode, string message)
            : base(statusCode, message) { }
    }
}