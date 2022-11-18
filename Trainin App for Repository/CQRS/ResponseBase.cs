namespace Trainin_App_for_Repository.CQRS
{
    public class ResponseBase<T> : ResponseBase
    {
        public T Data { get; set; }
    }

    public class ResponseBase
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }   
    }
}
