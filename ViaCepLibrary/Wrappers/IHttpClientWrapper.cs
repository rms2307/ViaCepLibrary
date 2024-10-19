namespace ViaCepLibrary.Wrappers
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}
