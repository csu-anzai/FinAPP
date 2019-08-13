using System;
using System.Net;

namespace BLL.Models.Exceptions
{
    public class ApiException : Exception
    {
        private HttpStatusCode unauthorized;

        public HttpStatusCode Code { get; }

        public ApiException(HttpStatusCode code, string message) : base(message)
        {
            Code = code;
        }

        public ApiException(HttpStatusCode code)
        {
            Code = code;
        }
    }
}
