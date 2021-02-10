using BankConverter.Business.Mappers;
using BankConverter.Business.Models;
using BankConverter.Business.ViewModels;
using Xunit;

namespace BankConverter.Tests.MappingTests
{
    public class RateMapperTests
    {
        [Fact]
        public void MapToViewModel_CorrectObject_FullObject()
        {
            // arrange
            var rateInput = new RateCalculation 
            {
                Rate = (decimal)2.51,
                RateValue = (decimal)2.5
            };
            var calculationInput = new CalculateRatesInputModel 
            {
                SecondCurrency = "USD",
                FirstCurrency = "LOT",
                Value = (decimal)12.3
            };

            // act
            var result = RateMapper.MapToViewModel(rateInput, calculationInput);

            // assert
            Assert.Equal(rateInput.RateValue, result.RateValue);
            Assert.Equal(rateInput.Rate, result.Rate);
            Assert.Equal(calculationInput.FirstCurrency, result.FirtsCurrency);
            Assert.Equal(calculationInput.SecondCurrency, result.SecondCurrency);
            Assert.Equal(calculationInput.Value, result.Amount);
        }
    }
}
