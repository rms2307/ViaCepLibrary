using System.Diagnostics.CodeAnalysis;

namespace ViaCepLibrary.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        public HttpClientWrapper(string baseUrl)
        {
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await _httpClient.GetAsync(requestUri).ConfigureAwait(false);
        }
    }
}
