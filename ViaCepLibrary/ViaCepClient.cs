using System.Net.Http.Json;
using ViaCepLibrary.Exceptions;
using ViaCepLibrary.Models;

namespace ViaCepLibrary
{
    public class ViaCepClient : IViaCepClient
    {
        private const string BASE_URL = "https://viacep.com.br/ws/{0}/json/";

        private readonly HttpClient _httpClient;

        public ViaCepClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AddressResult> GetAddressAsync(ZipCode zipCode)
        {
            Address? address;
            try
            {
                using HttpClient client = _httpClient;

                string endpoint = string.Format(BASE_URL, zipCode.ZipCodeNumber);
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

                response.EnsureSuccessStatusCode();
                address = await response.Content.ReadFromJsonAsync<Address>();
            }
            catch (Exception ex)
            {
                throw new ZipCodeException(ex.Message);
            }

            if (address is null || address.Error)
                throw new ZipCodeNotFoundException("Zip code number not found.");

            return new AddressResult
            {
                ZipCode = address.ZipCode,
                Street = address.Street,
                Complement = address.Complement,
                Neighborhood = address.Neighborhood,
                City = address.City,
                StateInitials = address.StateInitials,
                Unit = address.Unit,
                IBGECode = address.IBGECode,
            };
        }
    }
}
