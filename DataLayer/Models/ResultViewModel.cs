

using System.Net;

namespace DataLayer.Models
{
    public class ResultViewModel : ResultViewModel<object> { }

    public class ResultViewModel<T>
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; } 
        public bool IsSuccess { get; set; } = true;
    }
 
}
