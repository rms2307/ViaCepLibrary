namespace ViaCepLibrary
{
    public interface IViaCepClient
    {
        /// <summary>
        /// Searches the specified zip code.
        /// </summary>
        Task<AddressResult> GetAddressAsync(ZipCodeRequest request);

        /// <summary>
        /// Searches the specified address by state initials (UF), city and street name.
        /// </summary>
        Task<List<AddressResult>> GetZipCodeAsync(AddressRequest request);
    }
}