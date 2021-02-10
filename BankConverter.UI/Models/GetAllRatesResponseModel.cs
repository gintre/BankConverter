using System.Text.Json.Serialization;

namespace BankConverter.UI.Models
{
    public class GetAllRatesResponseModel
    {
        
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }

}
