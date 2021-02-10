using BankConverter.Business.Logic.Interfaces;
using BankConverter.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankConverter.Business.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RateController : Controller
    {
        private readonly IRateLogic _rateLogic;
        public RateController(IRateLogic rateLogic)
        {
            _rateLogic = rateLogic;
        }

        [HttpPost]
        public async Task<IActionResult> CalculateRates(CalculateRatesInputModel calculateRatesInputModel)
        {
            try
            {
                var result = await _rateLogic.CalculateRates(calculateRatesInputModel);

                return Ok(result);
            }
            catch
            {
                return BadRequest("Error unknown");
            }
        }
    }
}