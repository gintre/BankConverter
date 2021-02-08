using BankConverter.Business.Models;
using BankConverter.Business.ViewModels;
using System.Collections.Generic;

namespace BankConverter.Business.Mappers
{
    public static class RateMapper
    {
        public static List<GetAllRatesViewModel> MapToViewModel(List<RateItem> rateItems)
        {
            var result = new List<GetAllRatesViewModel>();

            foreach (var item in rateItems)
            {
                result.Add(MapToViewModel(item));
            }

            return result;
        }

        private static GetAllRatesViewModel MapToViewModel(RateItem rateItem)
        {
            return new GetAllRatesViewModel
            {
                Currency = rateItem.Currency,
                Value = rateItem.Value.ToString()
            };
        }
    }
}
