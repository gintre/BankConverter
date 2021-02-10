using BankConverter.Business.Exceptions;
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
            if (firstCurrency == null || secondCurrency == null)
            {
                throw new CurrencyNotFoundException();
            }

            var rateCalculation = new RateCalculation()
            {
                RateValue = CaclulateRateValue(firstCurrency.Value, secondCurrency.Value, calculateRatesInputModel.Value),
                Rate = CaclulateRate(firstCurrency.Value, secondCurrency.Value)
            };

            return RateMapper.MapToViewModel(rateCalculation, calculateRatesInputModel);
        }

        public decimal CaclulateRateValue(decimal rateFrom, decimal rateTo, decimal amount)
        {
            decimal rateValue = 0;
            try
            {
                rateValue = (amount / rateFrom) * rateTo;
            }
            catch
            {
                throw new DivideByZeroException();
            }

            return Math.Round(rateValue, 2);
        }

        public decimal CaclulateRate(decimal rateFrom, decimal rateTo)
        {
            decimal rate = 0;
            try
            {
                rate = (rateTo / rateFrom);
            }
            catch
            {
                throw new DivideByZeroException();
            }

            return Math.Round(rate, 4);
        }
    }
}
