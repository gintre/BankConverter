using BankConverter.Business.Models;
using BankConverter.Business.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankConverter.Business.Logic.Interfaces
{
    public interface IDataLoadLogic
    {
        Task<List<GetAllCurrenciesResponseModel>> GetCurrencies();

        Task<CurrencyItem> GetCurrency(string currencyName);
    }
}
