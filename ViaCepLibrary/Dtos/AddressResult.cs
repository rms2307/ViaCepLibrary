namespace ViaCepLibrary.Dtos
{
    public record AddressResult
    {
        public string ZipCode { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Complement { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string StateInitials { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int IBGECode { get; set; }
    }
}
