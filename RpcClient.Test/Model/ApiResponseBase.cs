namespace RpcClient.Test.Model
{
    public class ApiResponseBase<T>
    {
        public ApiStatus Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
