using BankConverter.Business.Models;
using BankConverter.Business.ViewModels;
using System.Collections.Generic;

namespace BankConverter.Business.Mappers
{
    public static class CurrenciesMapper
    {
        public static List<GetAllCurrenciesViewModel> MapToViewModel(List<CurrencyItem> rateItems)
        {
            var result = new List<GetAllCurrenciesViewModel>();

            foreach (var item in rateItems)
            {
                result.Add(MapToViewModel(item));
            }

            return result;
        }

        private static GetAllCurrenciesViewModel MapToViewModel(CurrencyItem rateItem)
        {
            return new GetAllCurrenciesViewModel
            {
                Currency = rateItem.Currency,
                Value = rateItem.Value.ToString()
            };
        }
    }
}
