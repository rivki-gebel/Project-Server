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

        private string myKey = "8a61348ec087f33a49bea33f";

        private HttpClient _client;
        public CurrenciesService()
        {
            _client = new HttpClient();
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
