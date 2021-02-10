using BankConverter.Business.Exceptions;
using BankConverter.Business.Logic;
using BankConverter.Business.ViewModels;
using BankConverter.Tests.TestsHelpers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BankConverter.Tests
{
    public class RateLogicTests
    {
        private RateLogic _rateLogic;

        [Fact]
        public async Task CalculateRates_WhenDataValid_ReturnRateObject()
        {
            //arrange
            _rateLogic = new RateLogic(MockHelper.GetDataLoadLogicWithMocks());
            var request = new CalculateRatesInputModel
            {
                FirstCurrency = "USD",
                SecondCurrency = "JPY",
                Value = 10
            };

            // act
            var result = await _rateLogic.CalculateRates(request);

            // assert
            Assert.Equal(request.FirstCurrency, result.FirtsCurrency);
            Assert.Equal(request.SecondCurrency, result.SecondCurrency);
            Assert.Equal(request.Value, result.Amount);
            Assert.Equal((decimal)1050.59, result.RateValue);
            Assert.Equal((decimal)105.0595, result.Rate);
        }

        [Fact]
        public async Task CalculateRates_WhenCurrencyDoesntExist_ReturnException()
        {
            //arrange
            _rateLogic = new RateLogic(MockHelper.GetDataLoadLogicWithMocks());
            var request = new CalculateRatesInputModel
            {
                FirstCurrency = "USD",
                SecondCurrency = "AAA",
                Value = 10
            };

            // assert
            await Assert.ThrowsAsync<CurrencyNotFoundException>(() => _rateLogic.CalculateRates(request));
        }

        [Fact]
        public void CaclulateRateValue_WhenDataCorrect_ReturnDecimal()
        {
            //arrange
            _rateLogic = new RateLogic(MockHelper.GetDataLoadLogicWithMocks());

            // act
            var result = _rateLogic.CaclulateRateValue(111, 222, 20);

            // assert
            Assert.Equal((decimal)40, result);
        }

        [Fact]
        public void CaclulateRateValue_WhenFirstArgZero_ReturnException()
        {
            //arrange
            _rateLogic = new RateLogic(MockHelper.GetDataLoadLogicWithMocks());

            // assert
            Assert.Throws<DivideByZeroException>(() => _rateLogic.CaclulateRateValue(0, 222, 20));
        }

        [Fact]
        public void CaclulateRateValue_WhenSecondZero_ReturnZero()
        {
            //arrange
            _rateLogic = new RateLogic(MockHelper.GetDataLoadLogicWithMocks());

            // act
            var result = _rateLogic.CaclulateRateValue(111, 0, 20);

            // assert
            Assert.Equal((decimal)0, result);
        }

        [Fact]
        public void CaclulateRateValue_WhenAmountZero_ReturnZero()
        {
            //arrange
            _rateLogic = new RateLogic(MockHelper.GetDataLoadLogicWithMocks());

            // act
            var result = _rateLogic.CaclulateRateValue(111, 0, 20);

            // assert
            Assert.Equal((decimal)0, result);
        }

        [Fact]
        public void CaclulateRate_WhenRateFromZero_ReturnException()
        {
            //arrange
            _rateLogic = new RateLogic(MockHelper.GetDataLoadLogicWithMocks());

            // assert
            Assert.Throws<DivideByZeroException>(() => _rateLogic.CaclulateRate(0, (decimal)1.23));
            
        }

        [Fact]
        public void CaclulateRate_WhenRateToZero_ReturnZero()
        {
            //arrange
            _rateLogic = new RateLogic(MockHelper.GetDataLoadLogicWithMocks());

            // act
            var result = _rateLogic.CaclulateRate((decimal)1.23, 0);

            // assert
            Assert.Equal((decimal)0, result);
        }

        [Fact]
        public void CaclulateRate_WhenDataValid_ReturnDecimal()
        {
            //arrange
            _rateLogic = new RateLogic(MockHelper.GetDataLoadLogicWithMocks());

            // act
            var result = _rateLogic.CaclulateRate((decimal)2.68, (decimal)1.23);

            // assert
            Assert.Equal((decimal)0.4590, result);
        }
    }
}
