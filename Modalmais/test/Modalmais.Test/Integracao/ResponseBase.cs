using System.Collections.Generic;
using System.Net;

namespace Modalmais.Test.Tests
{
    public class ResponseBase<T>
    {
        public ResponseBase(HttpStatusCode statusCode, bool success, T data, IEnumerable<string> errors)
        {
            Errors = new List<string>();
            StatusCode = statusCode;
            Success = success;
            Data = data;
            Errors = errors;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public bool Success { get; private set; }
        public T Data { get; private set; }
        public IEnumerable<string> Errors { get; private set; }
    }
}