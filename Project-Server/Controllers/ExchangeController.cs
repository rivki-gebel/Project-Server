using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_Server.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private string myKey = "8a61348ec087f33a49bea33f";

        private HttpClient _client;
        public ExchangeController()
        {
            _client = new HttpClient();
        }

        // GET: api/<ExchangeController>
        [HttpGet("{baseCurrency}")]
        public async Task<ActionResult> Get(string baseCurrency)
        {
            var response = await _client.GetAsync($"https://v6.exchangerate-api.com/v6/{myKey}/latest/{baseCurrency}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Conversion conversion = JsonConvert.DeserializeObject<Conversion>(content);
                return Ok(conversion);
            }

            return NotFound();
        }


    }
}
