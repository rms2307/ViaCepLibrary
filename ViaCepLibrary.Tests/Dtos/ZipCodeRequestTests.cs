using FluentAssertions;
using ViaCepLibrary.Dtos;

namespace ViaCepLibrary.Tests.Dtos
{
    public class ZipCodeRequestTests
    {
        [Fact]
        public void Constructor_ValidZipCode_ShouldCreateInstance()
        {
            // Arrange
            string validZipCode = "12345678";

            // Act
            ZipCodeRequest zipCodeRequest = new(validZipCode);

            // Assert
            zipCodeRequest.ZipCodeNumber.Should().Be(validZipCode);
        }

        [Fact]
        public void Constructor_ValidZipCodeWithSpecialCharacters_ShouldCreateInstanceWithCleanedZipCode()
        {
            // Arrange
            string zipCodeWithSpecialChars = "12.345-678";

            // Act
            ZipCodeRequest zipCodeRequest = new(zipCodeWithSpecialChars);

            // Assert
            zipCodeRequest.ZipCodeNumber.Should().Be("12345678");
        }

        [Fact]
        public void Constructor_NullZipCode_ShouldThrowArgumentNullException()
        {
            // Arrange
            string nullZipCode = null;

            // Act & Assert
            Action act = () => new ZipCodeRequest(nullZipCode);
            act.Should().Throw<ArgumentNullException>()
               .WithMessage("Value cannot be null. (Parameter 'ZipCodeNumber')");
        }

        [Fact]
        public void Constructor_EmptyZipCode_ShouldThrowArgumentNullException()
        {
            // Arrange
            string emptyZipCode = "";

            // Act & Assert
            Action act = () => new ZipCodeRequest(emptyZipCode);
            act.Should().Throw<ArgumentNullException>()
               .WithMessage("Value cannot be null. (Parameter 'ZipCodeNumber')");
        }

        [Fact]
        public void Constructor_WhitespaceZipCode_ShouldThrowArgumentNullException()
        {
            // Arrange
            string whitespaceZipCode = "   ";

            // Act & Assert
            Action act = () => new ZipCodeRequest(whitespaceZipCode);
            act.Should().Throw<ArgumentNullException>()
               .WithMessage("Value cannot be null. (Parameter 'ZipCodeNumber')");
        }

        [Theory]
        [InlineData("1234567")]
        [InlineData("123456789")]
        public void Constructor_InvalidZipCode_ShouldThrowArgumentException(string invalidZipCode)
        {
            // Act & Assert
            Action act = () => new ZipCodeRequest(invalidZipCode);
            act.Should().Throw<ArgumentException>()
               .WithMessage("The zip code number is invalid. Only eight numbers are allowed.");
        }
    }
}
