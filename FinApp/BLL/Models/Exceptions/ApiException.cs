using System;
using System.Net;

namespace BLL.Models.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode Code { get; private set; }
        public ApiException(HttpStatusCode statusCode)
        {
            Code = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            Code = statusCode;
        }
    }
}