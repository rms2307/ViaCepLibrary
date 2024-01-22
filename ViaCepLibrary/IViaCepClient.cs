namespace ViaCepLibrary
{
    public interface IViaCepClient
    {
        Task<AddressResult> GetAddressAsync(ZipCode cep);
    }
}