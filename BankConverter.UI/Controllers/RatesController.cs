using BankConverter.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            var result = new InputCalculateRatesViewModel();
            try
            {
                var response = await httpClient.GetAsync("https://localhost:44302/api/DataLoad/GetAllCurrencies");

                var rates = JsonSerializer.Deserialize<List<GetAllRatesResponseModel>>(response.Content.ReadAsStringAsync().Result);

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

        public async Task<IActionResult> CalculatedRates(InputCalculateRatesViewModel input)
        {
            var httpClient = new HttpClient();
            var result = new CalculateRatesResponseViewModel();
            try
            {
                var data = JsonSerializer.Serialize(input);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://localhost:44302/api/Rate/CalculateRates", content);

                result = JsonSerializer.Deserialize<CalculateRatesResponseViewModel>(response.Content.ReadAsStringAsync().Result);
            }
            catch
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Error converting rates" });
            }

            return View(result);
        }
    }
}
