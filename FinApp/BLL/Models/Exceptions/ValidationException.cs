using System.Net;

namespace BLL.Models.Exceptions
{
    public class ValidationException : CustomExeption
    {
        public object Parameter { get; set; }
        public HttpStatusCode ValidationErrorCode { get; private set; }

        public ValidationException() : base(HttpStatusCode.OK) { }

        public ValidationException(string message)
            : base(HttpStatusCode.OK, message)
        { }

        public ValidationException(HttpStatusCode code, string message)
            : base(HttpStatusCode.OK, message)
        {
            ValidationErrorCode = code;
        }

        public ValidationException(HttpStatusCode code, string message, object parameter)
            : this(code, message)
        {
            Parameter = parameter;
        }
    }
}
