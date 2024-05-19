using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_Server.Entities;
using Project_Server.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly ExchangeService _exchangeService;             
      
        public ExchangeController(ExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }
       
        // GET: api/<ExchangeController>
        [HttpGet("{baseCurrency}")]
        public async Task<ActionResult> Get(string baseCurrency)
        {
            Conversion conversion=await _exchangeService.GetExchangeRates(baseCurrency);
            if (conversion == null)
                return BadRequest();
            return Ok(conversion);
        }


    }
}
