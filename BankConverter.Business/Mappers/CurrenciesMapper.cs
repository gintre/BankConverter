using BankConverter.Business.Models;
using BankConverter.Business.ViewModels;
using System.Collections.Generic;

namespace BankConverter.Business.Mappers
{
    public static class CurrenciesMapper
    {
        public static List<GetAllCurrenciesResponseModel> MapToViewModel(List<CurrencyItem> rateItems)
        {
            var result = new List<GetAllCurrenciesResponseModel>();

            foreach (var item in rateItems)
            {
                result.Add(MapToViewModel(item));
            }

            return result;
        }

        public static GetAllCurrenciesResponseModel MapToViewModel(CurrencyItem rateItem)
        {
            return new GetAllCurrenciesResponseModel
            {
                Currency = rateItem.Currency,
                Value = rateItem.Value
            };
        }
    }
}
