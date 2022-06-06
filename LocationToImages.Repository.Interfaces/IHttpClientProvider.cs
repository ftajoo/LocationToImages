using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LocationToImages.Repository.Interfaces
{
    public interface IHttpClientProvider
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);

        Task<HttpResponseMessage> GetAsync(string requestUri, Action<HttpRequestHeaders> setDefaultRequestHeaders);

        Task<T> DeserializeObjectAsync<T>(HttpResponseMessage httpResponseMessage);
    }
}
