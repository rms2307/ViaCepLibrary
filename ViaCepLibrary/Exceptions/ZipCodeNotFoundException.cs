using System.Diagnostics.CodeAnalysis;

namespace ViaCepLibrary.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class ZipCodeNotFoundException : Exception
    {
        public ZipCodeNotFoundException() : base()
        {
        }

        public ZipCodeNotFoundException(string message) : base(message)
        {
        }
    }
}
