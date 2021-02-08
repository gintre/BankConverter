using BankConverter.Business.Exceptions;
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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankConverter.Business.Logic
{
    public class RatesLogic : IRatesLogic
    {

        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;
        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);
        public RatesLogic(IMemoryCache memoryCache, IConfiguration config)
        {
            _cache = memoryCache;
            _config = config;
        }

        public async Task<List<GetAllRatesViewModel>> LoadRates()
        {
                var rates = await GetRatesFromCache(ConfigurationConstants.RatesCacheKey, GetUsersSemaphore, () => GetRatesFromUrl());
                return RateMapper.MapToViewModel(rates);
        }

        private async Task<List<RateItem>> GetRatesFromUrl()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(_config.GetValue<string>("BusinessConstants:RatesUrl"));

            var result = await response.Content.ReadAsStringAsync();

            XDocument doc = XDocument.Parse(result);
            var matchingElements = doc.Descendants()
                          .Where(x => x.Attribute("currency") != null);

            var ratesList = new List<RateItem>();
            foreach (var item in matchingElements)
            {
                ratesList.Add(new RateItem
                {
                    Currency = item.Attribute("currency").Value,
                    Value = Convert.ToDecimal(item.Attribute("rate").Value, new CultureInfo("en-US"))
                });
            }

            return ratesList;
        }

        private async Task<List<RateItem>> GetRatesFromCache(string cacheKey, SemaphoreSlim semaphore, Func<Task<List<RateItem>>> func)
        {
            var rates = _cache.Get<List<RateItem>>(cacheKey);

            if (rates != null) return rates;
            try
            {
                await semaphore.WaitAsync();

                rates = _cache.Get<List<RateItem>>(cacheKey);

                if (rates != null) return rates;
                rates = await func();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(GetRatesReloadTime()));

                _cache.Set(cacheKey, rates, cacheEntryOptions);
            }
            finally
            {
                semaphore.Release();
            }

            return rates;
        }

        private int GetRatesReloadTime()
        {
            var defaultTimeValue = ConfigurationConstants.DefaultRatesReload;
            var timeFromCache = _config.GetValue<int>("BusinessConstants:ReloadRatesInMinutes");


            return (timeFromCache > 0) ? timeFromCache : defaultTimeValue;
        }
    }
}
