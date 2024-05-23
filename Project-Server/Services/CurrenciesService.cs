using Newtonsoft.Json;
using Project_Server.Entities;
using System;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;

namespace Project_Server.Services
{
    public class CurrenciesService
    {
        private static CurrenciesService _instance;

        private readonly IConfiguration _configuration;

        private string url;

        private HttpClient _client;

        public CurrenciesService(IConfiguration configuration)
        {
            _client = new HttpClient();
            _configuration = configuration;
            url = _configuration["URL-CURRENCIES"];
        }
             
        public async Task<List<Currency>> GetCurrenciesList()
        {

            HttpResponseMessage response = await _client.GetAsync(url);
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
    }
    
}
