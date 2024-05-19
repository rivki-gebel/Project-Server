﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_Server.Entities;

namespace Project_Server.Services
{
    public class ExchangeService
    {
        private static ExchangeService _instance;

        private string myKey = "8a61348ec087f33a49bea33f";

        private HttpClient _client;
        public ExchangeService()
        {
            _client = new HttpClient();
        }
        public async Task<Conversion> GetExchangeRates(string baseCurrency)
        {
            var response = await _client.GetAsync($"https://v6.exchangerate-api.com/v6/{myKey}/latest/{baseCurrency}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Conversion conversion = JsonConvert.DeserializeObject<Conversion>(content);
                return conversion;
            }

            return null;
        }
    }
}
