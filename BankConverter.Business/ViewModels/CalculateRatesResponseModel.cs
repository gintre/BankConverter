namespace BankConverter.Business.ViewModels
{
    public class CalculateRatesResponseModel
    {
        public decimal RateValue { get; set; }

        public decimal Rate { get; set; }

        public string FirtsCurrency { get; set; }

        public string SecondCurrency { get; set; }

        public decimal Amount { get; set; }
    }
}
