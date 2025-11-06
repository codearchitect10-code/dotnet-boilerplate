using System.Net;

namespace Framework.Core.Exceptions;
public class MyTemplateException : Exception
{
    public IEnumerable<string> ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }

    public MyTemplateException(string message, IEnumerable<string> errors, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }

    public MyTemplateException(string message) : base(message)
    {
        ErrorMessages = new List<string>();
    }
}
