using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BankConverter.UI.Models
{
    public class InputRatesViewModel
    {
        public List<GetAllRatesViewModel> Rates { get; set; }

        public IEnumerable<SelectListItem> CurrenciesToSelect { get; set; }

        public string FirstCurrency { get; set; }

        public string SecondCurrency { get; set; }

        public decimal Value { get; set; }
    }
}
