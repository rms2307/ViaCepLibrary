using System.Text.Json.Serialization;

namespace ViaCepLibrary.Models
{
    public record Address
    {
        [JsonPropertyName("cep")]
        public string ZipCode { get; set; } = string.Empty;

        [JsonPropertyName("logradouro")]
        public string Street { get; set; } = string.Empty;

        [JsonPropertyName("complemento")]
        public string Complement { get; set; } = string.Empty;

        [JsonPropertyName("bairro")]
        public string Neighborhood { get; set; } = string.Empty;

        [JsonPropertyName("localidade")]
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("uf")]
        public string StateInitials { get; set; } = string.Empty;

        [JsonPropertyName("unidade")]
        public string Unit { get; set; } = string.Empty;

        [JsonPropertyName("ibge")]
        public int IBGECode { get; set; }

        [JsonPropertyName("erro")]
        public bool Error { get; set; }
    }
}
