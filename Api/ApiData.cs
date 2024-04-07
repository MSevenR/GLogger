namespace GLogger.Api
{
    public class ApiData<T> : IApiData<T>
    {
        public string ErrorMessage { get; set; } = "";
        public T? ResponseData { get; set; } = default!;
    }
}