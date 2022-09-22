using Newtonsoft.Json;
using System.Net;

namespace TaskManager.Core.DTOs
{
    public class ResponseDTO<T>
    {
        public int StatusCode { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }

        public static ResponseDTO<T> Fail(string errorMessage, int statusCode = (int)HttpStatusCode.NotFound)
        {
            return new ResponseDTO<T> { Status = false, Message = errorMessage, StatusCode = statusCode };
        }
        public static ResponseDTO<T> Success(string successMessage, T data, int statusCode = (int)HttpStatusCode.OK)
        {
            return new ResponseDTO<T> { Status = true, Message = successMessage, Data = data, StatusCode = statusCode };
        }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
