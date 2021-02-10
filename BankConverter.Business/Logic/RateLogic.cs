using BankConverter.Business.Logic.Interfaces;
using BankConverter.Business.Mappers;
using BankConverter.Business.Models;
using BankConverter.Business.ViewModels;
using System;
using System.Threading.Tasks;

namespace BankConverter.Business.Logic
{
    public class RateLogic : IRateLogic
    {
        private readonly IDataLoadLogic _dataLoadLogic;
        public RateLogic(IDataLoadLogic dataLoadLogic)
        {
            _dataLoadLogic = dataLoadLogic;
        }

        public async Task<CalculateRatesResponseModel> CalculateRates(CalculateRatesInputModel calculateRatesInputModel)
        {
            var firstCurrency = await _dataLoadLogic.GetCurrency(calculateRatesInputModel.FirstCurrency);
            var secondCurrency = await _dataLoadLogic.GetCurrency(calculateRatesInputModel.SecondCurrency);

            var rateCalculation = new RateCalculation()
            {
                RateValue = CaclulateRateValue(firstCurrency.Value, secondCurrency.Value, calculateRatesInputModel.Value),
                Rate = CaclulateRateValue(firstCurrency.Value, secondCurrency.Value)
            };

            return RateMapper.MapToViewModel(rateCalculation, calculateRatesInputModel);
        }

        private decimal CaclulateRateValue(decimal rateFrom, decimal rateTo, decimal amount)
        {
            var rateValue = (amount / rateFrom) * rateTo;
            return Math.Round(rateValue, 2);
        }

        private decimal CaclulateRateValue(decimal rateFrom, decimal rateTo)
        {
            var rate = (rateTo / rateFrom);
            return Math.Round(rate, 4);
        }
    }
}
