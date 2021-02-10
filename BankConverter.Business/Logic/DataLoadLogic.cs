using BankConverter.Business.Logic.Interfaces;
using BankConverter.Business.Mappers;
using BankConverter.Business.Models;
using BankConverter.Business.Utils.Constants;
using BankConverter.Business.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankConverter.Business.Logic
{
    public class DataLoadLogic : IDataLoadLogic
    {

        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;
        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);
        public DataLoadLogic(IMemoryCache memoryCache, IConfiguration config)
        {
            _cache = memoryCache;
            _config = config;
        }

        public async Task<List<GetAllCurrenciesResponseModel>> GetCurrencies()
        {
                var rates = await GetCurrenciesFromCache(ConfigurationConstants.CurrenciesCacheKey, GetUsersSemaphore, () => GetCurrenciesFromUrl());
                return CurrenciesMapper.MapToViewModel(rates);
        }

        public async Task<CurrencyItem> GetCurrency(string currencyName)
        {
            var rates = await GetCurrenciesFromCache(ConfigurationConstants.CurrenciesCacheKey, GetUsersSemaphore, () => GetCurrenciesFromUrl());
            return rates.FirstOrDefault(x => x.Currency == currencyName);
        }

        private async Task<List<CurrencyItem>> GetCurrenciesFromUrl()
        {
            var httpClient = new HttpClient();
            
            var response = await httpClient.GetAsync(_config.GetValue<string>("BusinessConstants:CurrenciesUrl"));

            var result = await response.Content.ReadAsStringAsync();

            XDocument doc = XDocument.Parse(result);
            var matchingElements = doc.Descendants()
                          .Where(x => x.Attribute("currency") != null);

            var ratesList = new List<CurrencyItem>();
            foreach (var item in matchingElements)
            {
                ratesList.Add(new CurrencyItem
                {
                    Currency = item.Attribute("currency").Value,
                    Value = Convert.ToDecimal(item.Attribute("rate").Value, new CultureInfo("en-US"))
                });
            }

            return ratesList;
        }

        private async Task<List<CurrencyItem>> GetCurrenciesFromCache(string cacheKey, SemaphoreSlim semaphore, Func<Task<List<CurrencyItem>>> func)
        {
            var rates = _cache.Get<List<CurrencyItem>>(cacheKey);

            if (rates != null) return rates;
            try
            {
                await semaphore.WaitAsync();

                rates = _cache.Get<List<CurrencyItem>>(cacheKey);

                if (rates != null) return rates;
                rates = await func();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(GetCurrenciesReloadTime()));

                _cache.Set(cacheKey, rates, cacheEntryOptions);
            }
            finally
            {
                semaphore.Release();
            }

            return rates;
        }

        private int GetCurrenciesReloadTime()
        {
            var defaultTimeValue = ConfigurationConstants.DefaultCurrenciesReload;
            var timeFromCache = _config.GetValue<int>("BusinessConstants:ReloadCurrenciesInMinutes");


            return (timeFromCache > 0) ? timeFromCache : defaultTimeValue;
        }
    }
}
