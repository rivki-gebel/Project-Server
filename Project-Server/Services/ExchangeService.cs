using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_Server.Entities;

namespace Project_Server.Services
{
    public class ExchangeService
    {
        private static ExchangeService _instance;

        private readonly IConfiguration _configuration;

        private string url;
       


        private HttpClient _client;
        public ExchangeService(IConfiguration configuration)
        {
            _client = new HttpClient();
            _configuration = configuration;
            url = _configuration["URL-EXCHANGE"];
        }
        public async Task<Conversion> GetExchangeRates(string baseCurrency)
        {
            var response = await _client.GetAsync($"{url}/{baseCurrency}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Deserialize to a temporary dynamic object
                dynamic tempConversion = JsonConvert.DeserializeObject<dynamic>(content);

                // Create a new Conversion object
                Conversion conversion = new Conversion
                {
                    BaseCode = tempConversion.base_code
                };

                // Iterate over the conversion_rates and create ExchangeRates objects
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
