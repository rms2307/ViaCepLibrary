using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace ViaCepLibrary.Dtos
{
    public class ZipCode
    {
        public ZipCode(string zipCodeNumber)
        {
            zipCodeNumber = ClearZipCodeNumber(zipCodeNumber);
            Validate(zipCodeNumber);

            ZipCodeNumber = zipCodeNumber;
        }

        private string ClearZipCodeNumber(string zipCodeNumber)
        {
            return Regex.Replace(zipCodeNumber, @"[^\d]", "");
        }

        private void Validate(string zipCodeNumber)
        {
            if (string.IsNullOrWhiteSpace(zipCodeNumber))
                throw new ArgumentNullException("Zip code number cannot be null or empty.");

            Regex regex = new(@"^\d{8}$");
            if (!regex.IsMatch(zipCodeNumber))
                throw new ArgumentException("The zip code number is invalid. Only eight numbers are allowed.");
        }

        public string ZipCodeNumber { get; private set; }
    }
}