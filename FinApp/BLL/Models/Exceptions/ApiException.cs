using System;
using System.Net;

namespace BLL.Models.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public ApiException(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}
