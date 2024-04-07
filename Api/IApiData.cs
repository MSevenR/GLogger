namespace GLogger.Api
{
    public interface IApiData<T>
    {
        public string ErrorMessage { get; set; }
        public T? ResponseData { get; set; }
    }
}