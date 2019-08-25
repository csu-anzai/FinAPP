using System;
using System.Net;

namespace BLL.Models.Exceptions
{
    public class CustomExeption : Exception
    {
        public HttpStatusCode Code { get; set; }

        public CustomExeption(HttpStatusCode statusCode)
        {
            Code = statusCode;
        }

        public CustomExeption(HttpStatusCode statusCode, string message)
            : base(message)
        {
            Code = statusCode;
        }
    }
}
