using BankConverter.Business.ViewModels;
using System.Threading.Tasks;

namespace BankConverter.Business.Logic.Interfaces
{
    public interface IRateLogic
    {
        Task<CalculateRatesResponseModel> CalculateRates(CalculateRatesInputModel calculateRatesInputModel);
    }
}
