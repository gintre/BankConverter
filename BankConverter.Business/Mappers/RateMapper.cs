using BankConverter.Business.Models;
using BankConverter.Business.ViewModels;

namespace BankConverter.Business.Mappers
{
    public static class RateMapper
    {
        public static CalculateRatesResponseModel MapToViewModel(RateCalculation rateItem, CalculateRatesInputModel rateInput)
        {
            var result = new CalculateRatesResponseModel()
            { 
                Rate = rateItem.Rate,
                RateValue = rateItem.RateValue,
                Amount = rateInput.Value,
                FirtsCurrency = rateInput.FirstCurrency,
                SecondCurrency = rateInput.SecondCurrency
            };

            return result;
        }
    }
}
