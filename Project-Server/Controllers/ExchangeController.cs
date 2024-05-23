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
        private readonly Service _service;             
      
        public ExchangeController(Service service)
        {
            _service = service;
        }
       
        // GET: api/<ExchangeController>
        [HttpGet("{baseCurrency}")]
        public async Task<ActionResult> Get(string baseCurrency)
        {
            Conversion conversion=await _service.GetExchangeRates(baseCurrency);
            if (conversion == null)
                return BadRequest();
            return Ok(conversion);
        }


    }
}
