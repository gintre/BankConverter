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
            var result = new InputRatesViewModel();
            try
            {
                var response = await httpClient.GetAsync("https://localhost:44302/api/DataLoad/GetAllCurrencies");

                var rates = JsonSerializer.Deserialize<List<GetAllRatesViewModel>>(response.Content.ReadAsStringAsync().Result);

                result.Rates = rates;
                result.CurrenciesToSelect = rates.
                        Select(i => new SelectListItem()
                        {
                            Text = i.Currency.ToString(),
                            Value = i.Currency
                        });
            }
            catch
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Error loading data"});
            }

            return View(result);
        }

        public async Task<IActionResult> CalculateRates(InputRatesViewModel input)
        {
            var httpClient = new HttpClient();

            return View();
        }
    }
}
