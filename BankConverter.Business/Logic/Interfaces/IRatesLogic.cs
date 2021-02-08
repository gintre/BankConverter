using BankConverter.Business.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankConverter.Business.Logic.Interfaces
{
    public interface IRatesLogic
    {
        Task<List<GetAllRatesViewModel>> LoadRates();
    }
}
