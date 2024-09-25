using FluentAssertions;
using ViaCepLibrary.Dtos;

namespace ViaCepLibrary.Tests.Dtos
{
    public class AddressRequestTests
    {
        [Fact]
        public void Constructor_DadosValidos_DeveCriarInstancia()
        {
            // Arrange
            string stateInitials = "SP";
            string city = "São Paulo";
            string street = "Avenida Paulista";

            // Act
            var addressRequest = new AddressRequest(stateInitials, city, street);

            // Assert
            addressRequest.Should().NotBeNull();
            addressRequest.StateInitials.Should().Be(stateInitials);
            addressRequest.City.Should().Be(city);
            addressRequest.Street.Should().Be(street);
        }

        [Fact]
        public void Constructor_StateInitialsNulo_DeveLancarArgumentNullException()
        {
            // Arrange
            string stateInitials = null;
            string city = "São Paulo";
            string street = "Avenida Paulista";

            // Act & Assert
            Action act = () => new AddressRequest(stateInitials, city, street);
            act.Should().Throw<ArgumentNullException>().WithMessage("*State initials cannot be null or empty.*");
        }

        [Fact]
        public void Constructor_CityNulo_DeveLancarArgumentNullException()
        {
            // Arrange
            string stateInitials = "SP";
            string city = null;
            string street = "Avenida Paulista";

            // Act & Assert
            Action act = () => new AddressRequest(stateInitials, city, street);
            act.Should().Throw<ArgumentNullException>().WithMessage("*City cannot be null or empty.*");
        }

        [Fact]
        public void Constructor_StreetNulo_DeveLancarArgumentNullException()
        {
            // Arrange
            string stateInitials = "SP";
            string city = "São Paulo";
            string street = null;

            // Act & Assert
            Action act = () => new AddressRequest(stateInitials, city, street);
            act.Should().Throw<ArgumentNullException>().WithMessage("*Street cannot be null or empty.*");
        }

        [Fact]
        public void Constructor_StateInitialsVazio_DeveLancarArgumentNullException()
        {
            // Arrange
            string stateInitials = "";
            string city = "São Paulo";
            string street = "Avenida Paulista";

            // Act & Assert
            Action act = () => new AddressRequest(stateInitials, city, street);
            act.Should().Throw<ArgumentNullException>().WithMessage("*State initials cannot be null or empty.*");
        }

        [Fact]
        public void Constructor_CityVazio_DeveLancarArgumentNullException()
        {
            // Arrange
            string stateInitials = "SP";
            string city = "";
            string street = "Avenida Paulista";

            // Act & Assert
            Action act = () => new AddressRequest(stateInitials, city, street);
            act.Should().Throw<ArgumentNullException>().WithMessage("*City cannot be null or empty.*");
        }

        [Fact]
        public void Constructor_StreetVazio_DeveLancarArgumentNullException()
        {
            // Arrange
            string stateInitials = "SP";
            string city = "São Paulo";
            string street = "";

            // Act & Assert
            Action act = () => new AddressRequest(stateInitials, city, street);
            act.Should().Throw<ArgumentNullException>().WithMessage("*Street cannot be null or empty.*");
        }
    }
}
