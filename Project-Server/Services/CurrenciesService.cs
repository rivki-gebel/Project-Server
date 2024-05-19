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

        private string myKey;

        private HttpClient _client;

        public CurrenciesService(IConfiguration configuration)
        {
            _client = new HttpClient();
            _configuration = configuration;
            myKey = _configuration["MY-API-KEY"];
        }
             
        public async Task<CurrenciesList> GetCurrenciesList()
        {

            HttpResponseMessage response = await _client.GetAsync($"https://v6.exchangerate-api.com/v6/{myKey}/codes");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                CurrenciesList list = JsonConvert.DeserializeObject<CurrenciesList>(content);
                return list;
            }

            return null;
        }
    }
    
}
