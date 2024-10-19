using FluentAssertions;
using System.Net.Http.Json;
using ViaCepLibrary.Dtos;
using ViaCepLibrary.Models;
using ViaCepLibrary.Wrappers;
using NSubstitute;
using ViaCepLibrary.Exceptions;

namespace ViaCepLibrary.Tests.Client
{
    public class ViaCepClientTests
    {
        private readonly IHttpClientWrapper _httpClientWrapperMock;

        private readonly IViaCepClient _viaCepClient;

        public ViaCepClientTests()
        {
            _httpClientWrapperMock = Substitute.For<IHttpClientWrapper>();

            _viaCepClient = new ViaCepClient(_httpClientWrapperMock);
        }

        [Fact]
        public async Task GetAddress_ValidZipCode_ShouldReturnAddressResult()
        {
            // Arrange
            var expectedAddress = new Address
            {
                ZipCode = "01001-000",
                Street = "Praça da Sé",
                City = "São Paulo",
                StateInitials = "SP"
            };
            var zipCodeRequest = new ZipCodeRequest("01001-000");

            var httpResponse = new HttpResponseMessage
            {
                Content = JsonContent.Create(expectedAddress),
                StatusCode = System.Net.HttpStatusCode.OK
            };
            _httpClientWrapperMock.GetAsync(Arg.Any<string>()).Returns(httpResponse);

            // Act
            var result = await _viaCepClient.GetAddressAsync(zipCodeRequest);

            // Assert
            result.Should().NotBeNull();
            result.ZipCode.Should().Be(expectedAddress.ZipCode);
            result.Street.Should().Be(expectedAddress.Street);
            result.City.Should().Be(expectedAddress.City);
            result.StateInitials.Should().Be(expectedAddress.StateInitials);
        }

        [Fact]
        public async Task GetAddress_InvalidZipCode_ShouldThrowZipCodeNotFoundException()
        {
            // Arrange
            var expectedAddress = new Address
            {
                Error = true
            };
            var zipCodeRequest = new ZipCodeRequest("01001-000");

            var httpResponse = new HttpResponseMessage
            {
                Content = JsonContent.Create(expectedAddress),
                StatusCode = System.Net.HttpStatusCode.OK
            };
            _httpClientWrapperMock.GetAsync(Arg.Any<string>()).Returns(httpResponse);

            // Act
            Func<Task> action = async () => await _viaCepClient.GetAddressAsync(zipCodeRequest);

            // Assert
            await action.Should().ThrowAsync<ZipCodeNotFoundException>().WithMessage("Zip code number not found.");
        }

        [Fact]
        public async Task GetZipCode_ValidAddress_ShouldReturnListOfAddressResults()
        {
            // Arrange
            var addressList = new List<Address>
        {
            new() { ZipCode = "01001-000", Street = "Praça da Sé", City = "São Paulo", StateInitials = "SP" }
        };
            var addressRequest = new AddressRequest("SP", "São Paulo", "Praça da Sé");

            var httpResponse = new HttpResponseMessage
            {
                Content = JsonContent.Create(addressList),
                StatusCode = System.Net.HttpStatusCode.OK
            };
            _httpClientWrapperMock.GetAsync(Arg.Any<string>()).Returns(httpResponse);

            // Act
            var result = await _viaCepClient.GetZipCodeAsync(addressRequest);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.First().ZipCode.Should().Be(addressList.First().ZipCode);
            result.First().City.Should().Be(addressList.First().City);
        }

        [Fact]
        public async Task GetZipCode_NoResults_ShouldThrowZipCodeNotFoundException()
        {
            // Arrange
            var addressRequest = new AddressRequest("SP", "São Paulo", "Rua Inexistente");

            var httpResponse = new HttpResponseMessage
            {
                Content = JsonContent.Create(new List<Address>()),
                StatusCode = System.Net.HttpStatusCode.OK
            };
            _httpClientWrapperMock.GetAsync(Arg.Any<string>()).Returns(httpResponse);

            // Act
            Func<Task> action = async () => await _viaCepClient.GetZipCodeAsync(addressRequest);

            // Assert
            await action.Should().ThrowAsync<ZipCodeNotFoundException>().WithMessage("Address not found.");
        }
    }
}