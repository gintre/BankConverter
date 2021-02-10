using System;

namespace BankConverter.Business.Exceptions
{
    public class CurrencyNotFoundException : Exception
    {
        public CurrencyNotFoundException() : base("Currency not found")
        {        }
    }
}
