using BankConverter.Business.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankConverter.Business.Logic.Interfaces
{
    public interface IDataLoadLogic
    {
        Task<List<GetAllCurrenciesViewModel>> LoadCurrencies();
    }
}
