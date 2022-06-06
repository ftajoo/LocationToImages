using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LocationToImages.Repository.HttpClientProvider
{
    public class HttpClientProvider : Interfaces.IHttpClientProvider
    {
        private readonly HttpClient httpClient;

        private readonly DefaultContractResolver contractResolver;
        private readonly JsonSerializerSettings jsonSerializerSettings;

        public HttpClientProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;

            // Deserialize content from snake case naming to pascal case
            contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy
                {
                    OverrideSpecifiedNames = false
                }
            };

            jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            };
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return httpClient.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri, 
            Action<HttpRequestHeaders> setDefaultRequestHeaders)
        {
            httpClient.DefaultRequestHeaders.Clear();
            setDefaultRequestHeaders(httpClient.DefaultRequestHeaders);
            return httpClient.GetAsync(requestUri);
        }

        public async Task<T> DeserializeObjectAsync<T>(HttpResponseMessage httpResponseMessage)
        {
            string content = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content, jsonSerializerSettings);
        }
    }
}
