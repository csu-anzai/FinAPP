using System.Net;

namespace BLL.Models.Exceptions
{
    public class ValidationExeption : CustomExeption
    {
        public object Parameter { get; set; }
        public HttpStatusCode ValidationErrorCode { get; private set; }

        public ValidationExeption() : base(HttpStatusCode.OK) { }

        public ValidationExeption(HttpStatusCode code, string message)
            : base(HttpStatusCode.OK, message)
        {
            ValidationErrorCode = code;
        }

        public ValidationExeption(HttpStatusCode code, string message, object parameter)
            : this(code, message)
        {
            Parameter = parameter;
        }
    }
}
