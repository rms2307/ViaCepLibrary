using System.Net.Http.Json;
using ViaCepLibrary.Exceptions;
using ViaCepLibrary.Models;
using ViaCepLibrary.Wrappers;

namespace ViaCepLibrary
{
    public class ViaCepClient : IViaCepClient
    {
        private const string BASE_URL = "https://viacep.com.br/ws/";

        private readonly IHttpClientWrapper _httpClientWrapper;

        public ViaCepClient(IHttpClientWrapper? httpClientWrapper = null)
        {
            _httpClientWrapper = httpClientWrapper ?? new HttpClientWrapper(BASE_URL);
        }

        public async Task<AddressResult> GetAddressAsync(ZipCodeRequest zipCode)
        {
            Address? address;
            try
            {
                HttpResponseMessage response = await _httpClientWrapper.GetAsync($"{zipCode.ZipCodeNumber}/json/");

                response.EnsureSuccessStatusCode();
                address = await response.Content.ReadFromJsonAsync<Address>().ConfigureAwait(false);
            }
            catch (HttpRequestException ex)
            {
                throw new ZipCodeException("Error fetching data from ViaCep.", ex);
            }
            catch (Exception ex)
            {
                throw new ZipCodeException(ex.Message, ex);
            }

            if (address is null)
                throw new ZipCodeNotFoundException("Zip code number not found.");

            if (address.Error?.ToLower() == "true")
                throw new ZipCodeException("Error fetching data from ViaCep.");

            return MapToAddressResult(address);
        }

        public async Task<List<AddressResult>> GetZipCodeAsync(AddressRequest request)
        {
            List<Address>? addresses;
            try
            {
                HttpResponseMessage response = await _httpClientWrapper.GetAsync($"{request.StateInitials}/{request.City}/{request.Street}/json/");

                response.EnsureSuccessStatusCode();
                addresses = await response.Content.ReadFromJsonAsync<List<Address>>().ConfigureAwait(false);
            }
            catch (HttpRequestException ex)
            {
                throw new ZipCodeException("Error fetching data from ViaCep.", ex);
            }
            catch (Exception ex)
            {
                throw new ZipCodeException(ex.Message, ex);
            }

            if (addresses is null || addresses.Count == 0)
                throw new ZipCodeNotFoundException("Address not found.");

            if (addresses.Any(a => a.Error?.ToLower() == "true"))
                throw new ZipCodeException("Error fetching data from ViaCep.");

            return addresses.Select(MapToAddressResult).ToList();
        }

        private AddressResult MapToAddressResult(Address address)
        {
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
