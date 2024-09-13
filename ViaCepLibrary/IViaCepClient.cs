namespace ViaCepLibrary
{
    public interface IViaCepClient
    {
        Task<AddressResult> GetAddressAsync(ZipCodeRequest cep);
        Task<List<AddressResult>> GetZipCodeAsync(AddressRequest request);
    }
}