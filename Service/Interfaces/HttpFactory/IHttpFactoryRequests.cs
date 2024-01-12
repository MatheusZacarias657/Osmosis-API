namespace Service.Interfaces.HttpFactory
{
    public interface IHttpFactoryRequests
    {
        Uri BuildUri(string baseUrl, Dictionary<string, string> parameters);
        Uri BuildUri(string baseUrl, string endpoint);
        Uri BuildUri(string baseUrl, string endpoint, Dictionary<string, string> parameters);
        Task<HttpResponseMessage> DefaultPostRequest<T>(Uri url, T body);
        Task<HttpResponseMessage> DefaultPostRequest<T>(Uri url, T body, Dictionary<string, string> headers);
        T ProcessResponse<T>(HttpResponseMessage response);
    }
}