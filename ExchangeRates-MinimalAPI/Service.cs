
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

        private readonly string _currenciesUrl;

        private readonly string _exchangeUrl;

        private HttpClient Client;

        public Service(IConfiguration configuration)
        {
            Client = new HttpClient();
            _configuration = configuration;
            _currenciesUrl = _configuration["URL-CURRENCIES"];
            _exchangeUrl = _configuration["URL-EXCHANGE"];
        }

        public async Task<List<Currency>> GetCurrenciesList()
        {

            HttpResponseMessage response = await Client.GetAsync(_currenciesUrl);
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
            HttpResponseMessage response = await Client.GetAsync($"{_exchangeUrl}/{baseCurrency}");
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
