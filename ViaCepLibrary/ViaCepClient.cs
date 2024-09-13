using System.Net.Http.Json;
using ViaCepLibrary.Exceptions;
using ViaCepLibrary.Models;

namespace ViaCepLibrary
{
    public class ViaCepClient : IViaCepClient
    {
        private const string BASE_URL = "https://viacep.com.br/ws/";

        private readonly HttpClient _httpClient;

        public ViaCepClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BASE_URL);
        }

        public async Task<AddressResult> GetAddressAsync(ZipCodeRequest zipCode)
        {
            Address? address;
            try
            {
                using HttpClient client = _httpClient;
                HttpResponseMessage response = await _httpClient.GetAsync($"{zipCode.ZipCodeNumber}/json/");

                response.EnsureSuccessStatusCode();
                address = await response.Content.ReadFromJsonAsync<Address>().ConfigureAwait(false);
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

        public async Task<List<AddressResult>> GetZipCodeAsync(AddressRequest request)
        {
            List<Address>? addresses;
            try
            {
                using HttpClient client = _httpClient;
                HttpResponseMessage response = await _httpClient.GetAsync($"{request.StateInitials}/{request.City}/{request.Street}/json/");

                response.EnsureSuccessStatusCode();
                addresses = await response.Content.ReadFromJsonAsync<List<Address>>().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (addresses is null || !addresses.Any())
                throw new ZipCodeNotFoundException("Address not found.");

            return addresses.Select(a => new AddressResult()
            {
                ZipCode = a.ZipCode,
                Street = a.Street,
                Complement = a.Complement,
                Neighborhood = a.Neighborhood,
                City = a.City,
                StateInitials = a.StateInitials,
                Unit = a.Unit,
                IBGECode = a.IBGECode,
            }).ToList();
        }
    }
}
