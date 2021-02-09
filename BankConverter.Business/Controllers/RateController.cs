﻿using BankConverter.Business.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankConverter.Business.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RateController : Controller
    {
        private readonly IDataLoadLogic _dataLoadLogic;
        public RateController(IDataLoadLogic dataLoadLogic)
        {
            _dataLoadLogic = dataLoadLogic;
        }

        [HttpGet]
        public async Task<IActionResult> CalculateRates()
        {
            try
            {
                var result = await _dataLoadLogic.LoadCurrencies();

                return Ok(result);
            }
            catch
            {
                return BadRequest("Error unknown");
            }
        }
    }
}