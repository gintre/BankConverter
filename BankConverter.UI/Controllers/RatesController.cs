using BankConverter.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankConverter.UI.Controllers
{
    public class RatesController : Controller
    {
        public RatesController()
        {        }

        public async Task<IActionResult> LoadRates()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://localhost:44302/rate/GetAllRates");

            var rates = JsonSerializer.Deserialize<List<GetAllRatesViewModel>>(response.Content.ReadAsStringAsync().Result);

            var result = new InputRatesViewModel()
            { 
                Rates = rates,
                CurrenciesToSelect = rates.
                    Select(i => new SelectListItem()
                    {
                        Text = i.Currency.ToString(),
                        Value = i.Currency
                    })
            };

            return View(result);
        }

        public async Task<IActionResult> CalculateRates(InputRatesViewModel input)
        {
            return View();
        }
    }
}
