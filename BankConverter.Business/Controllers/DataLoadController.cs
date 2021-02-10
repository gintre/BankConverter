using System.Threading.Tasks;
using BankConverter.Business.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankConverter.Business.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DataLoadController : Controller
    {
        private readonly IDataLoadLogic _dataLoadLogic;

        public DataLoadController(IDataLoadLogic dataLoadLogic)
        {
            _dataLoadLogic = dataLoadLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCurrencies()
        {
            try
            {
                var result = await _dataLoadLogic.GetCurrencies();

                return Ok(result);
            }
            catch 
            {
                return BadRequest("Error unknown");
            }
        }
    }
}
