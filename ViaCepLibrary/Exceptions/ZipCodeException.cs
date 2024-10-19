using System.Diagnostics.CodeAnalysis;

namespace ViaCepLibrary.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class ZipCodeException : Exception
    {
        public ZipCodeException() : base()
        {
        }

        public ZipCodeException(string message) : base(message)
        {
        }

        public ZipCodeException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
