using BankConverter.Business.Logic;
using BankConverter.Business.Logic.Interfaces;
using BankConverter.Business.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankConverter.Tests.TestsHelpers
{
    public static class MockHelper
    {
        public static IDataLoadLogic GetDataLoadLogicWithMocks()
        {
            var dataLoadLogicMock = Substitute.For<IDataLoadLogic>();

            var firstCurrency = new CurrencyItem()
            {
                Currency = "USD",
                Value = (decimal)1.2017
            };

            var secondCurrency = new CurrencyItem()
            {
                Currency = "JPY",
                Value = (decimal)126.25
            };

            dataLoadLogicMock.GetCurrency("USD").Returns(firstCurrency);
            dataLoadLogicMock.GetCurrency("JPY").Returns(secondCurrency);

            return dataLoadLogicMock;
        }
    }
}
