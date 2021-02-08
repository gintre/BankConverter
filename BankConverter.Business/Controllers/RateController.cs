using System.Threading.Tasks;
using BankConverter.Business.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankConverter.Business.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RateController : Controller
    {
        private readonly IRatesLogic _ratesLogic;

        public RateController(IRatesLogic ratesLogic)
        {
            _ratesLogic = ratesLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRates()
        {
            try
            {
                var result = await _ratesLogic.LoadRates();

                return Ok(result);
            }
            catch 
            {
                return BadRequest("Error unknown");
            }
        }
    }
}
