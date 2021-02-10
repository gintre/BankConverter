using BankConverter.Business.Mappers;
using BankConverter.Business.Models;
using System.Collections.Generic;
using Xunit;

namespace BankConverter.Tests.MappingTests
{
    public class CurrenciesMapperTests
    {
        [Fact]
        public void MapToViewModelTest_CorrectData_ReturnList()
        {
            // arrange
            var input = new List<CurrencyItem>();
            input.Add(new CurrencyItem { Currency = "USD", Value = (decimal)2.12 });
            input.Add(new CurrencyItem { Currency = "JPY", Value = (decimal)2.5 });

            // act
            var result = CurrenciesMapper.MapToViewModel(input);

            // assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void MapToViewModelTest_EmptyList()
        {
            // arrange
            var input = new List<CurrencyItem>();

            // act
            var result = CurrenciesMapper.MapToViewModel(input);

            // assert
            Assert.Empty(result);
        }

        [Fact]
        public void MapToViewModelTest_CorrectData_ReturnItem()
        {
            // arrange
            var input = new CurrencyItem { Currency = "USD", Value = (decimal)2.12 };

            // act
            var result = CurrenciesMapper.MapToViewModel(input);

            // assert
            Assert.Equal(input.Value, result.Value);
            Assert.Equal(input.Currency, result.Currency);
        }
    }
}

