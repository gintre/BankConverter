using System.Text.Json.Serialization;

namespace BankConverter.UI.Models
{
    public class CalculateRatesResponseViewModel
    {
        [JsonPropertyName("rateValue")]
        public decimal RateValue { get; set; }

        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }

        [JsonPropertyName("firtsCurrency")]
        public string FirtsCurrency { get; set; }

        [JsonPropertyName("secondCurrency")]
        public string SecondCurrency { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}
