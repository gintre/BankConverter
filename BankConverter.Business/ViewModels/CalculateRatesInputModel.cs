namespace BankConverter.Business.ViewModels
{
    public class CalculateRatesInputModel
    {
        public string FirstCurrency { get; set; }

        public string SecondCurrency { get; set; }

        public decimal Value { get; set; }
    }
}
