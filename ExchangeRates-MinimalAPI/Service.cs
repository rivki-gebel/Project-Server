
using Newtonsoft.Json;
using Project_Server.Entities;
using System;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;

namespace Project_Server
{
    public class Service
    {        
        private readonly IConfiguration _configuration;

        private string CurrenciesUrl;

        private string ExchangeUrl;

        private HttpClient _client;

        public Service(IConfiguration configuration)
        {
            _client = new HttpClient();
            _configuration = configuration;
            CurrenciesUrl = _configuration["URL-CURRENCIES"];
            ExchangeUrl = _configuration["URL-EXCHANGE"];
        }

        public async Task<List<Currency>> GetCurrenciesList()
        {

            HttpResponseMessage response = await _client.GetAsync(CurrenciesUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                dynamic tempCurrenciesList = JsonConvert.DeserializeObject<dynamic>(content);

                var currencies = new List<Currency>();
                foreach (var code in tempCurrenciesList.supported_codes)
                {
                    currencies.Add(new Currency { Code = code[0], Name = code[1] });
                }

                return currencies;
            }

            return null;
        }

        public async Task<Conversion> GetExchangeRates(string baseCurrency)
        {
            var response = await _client.GetAsync($"{ExchangeUrl}/{baseCurrency}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                dynamic tempConversion = JsonConvert.DeserializeObject<dynamic>(content);

                Conversion conversion = new Conversion
                {
                    BaseCode = tempConversion.base_code
                };

                foreach (var rate in tempConversion.conversion_rates)
                {
                    conversion.ConversionRates.Add(new ExchangeRates
                    {
                        TargetCode = rate.Name,
                        ExchangeRate = (double)rate.Value
                    });
                }

                return conversion;
            }

            return null;
        }

    }
}
