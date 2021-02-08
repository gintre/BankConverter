using System.Text.Json.Serialization;

namespace BankConverter.UI.Models
{
    public class GetAllRatesViewModel
    {
        
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

}
