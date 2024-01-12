using Service.Interfaces.HttpFactory;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;

namespace Service.Services.HttpFactory
{
    public class HttpFactoryRequests : IHttpFactoryRequests
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HttpFactoryRequests(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public Uri BuildUri(string baseUrl, string endpoint) => BuildUri(baseUrl, endpoint, null);
        public Uri BuildUri(string baseUrl, Dictionary<string, string> parameters) => BuildUri(baseUrl, null, parameters);
        public Uri BuildUri(string baseUrl, string endpoint, Dictionary<string, string> parameters)
        {
            var builder = new UriBuilder(baseUrl);

            if (!string.IsNullOrEmpty(endpoint))
                builder.Path = endpoint;

            if (parameters != null && parameters.Count > 0)
            {
                var queryBuilder = new List<string>();

                foreach (var parameter in parameters)
                {
                    string encodedKey = HttpUtility.UrlEncode(parameter.Key);
                    string encodedValue = HttpUtility.UrlEncode(parameter.Value);
                    string queryParameter = $"{encodedKey}={encodedValue}";

                    queryBuilder.Add(queryParameter);
                }

                builder.Query = string.Join("&", queryBuilder);
            }

            return builder.Uri;
        }

        public T ProcessResponse<T>(HttpResponseMessage response)
        {
            Stream streamData = response.Content.ReadAsStreamAsync().Result;
            StreamReader reader = new StreamReader(streamData);
            string result = reader.ReadToEnd();

            try
            {
                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch
            {
                return JsonSerializer.Deserialize<T>(result);
            }
        }

        public async Task<HttpResponseMessage> DefaultPostRequest<T>(Uri url, T body) => await DefaultPostRequest(url, body, null);
        public async Task<HttpResponseMessage> DefaultPostRequest<T>(Uri url, T body, Dictionary<string, string> headers)
        {
            try
            {
                HttpClient request = httpClientFactory.CreateClient();
                StringContent content = new StringContent(JsonSerializer.Serialize(body));
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        request.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                HttpResponseMessage response = await request.PostAsync(url, content);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
