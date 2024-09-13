namespace ViaCepLibrary.Dtos
{
    public record AddressRequest
    {
        public AddressRequest(string stateInitials, string city, string street)
        {
            Validate(stateInitials, city, street);

            StateInitials = stateInitials;
            City = city;
            Street = street;
        }

        public string StateInitials { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string Street { get; private set; } = string.Empty;

        private void Validate(string stateInitials, string city, string street)
        {
            if (string.IsNullOrWhiteSpace(stateInitials))
                throw new ArgumentNullException("State initials cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException("City cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentNullException("Street cannot be null or empty.");
        }
    }
}
