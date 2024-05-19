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
        private readonly IConfiguration _configuration;

        private string myKey; 

        private HttpClient _client;
        public ExchangeController(IConfiguration configuration)
        {
            _client = new HttpClient();
            _configuration = configuration;
            myKey = _configuration["MY-API-KEY"];
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
