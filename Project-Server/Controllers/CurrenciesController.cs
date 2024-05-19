using Microsoft.AspNetCore.Mvc;
using Project_Server.Entities;
using Project_Server.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_Server.Controllers
{
        
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly CurrenciesService _currenciesService;

        public CurrenciesController(CurrenciesService currenciesService)
        {
            _currenciesService = currenciesService;
        }

        // GET: api/<CurrenciesController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            CurrenciesList list=await _currenciesService.GetCurrenciesList();
            if (list == null)
                return BadRequest();
            return Ok(list);
        }


    }
}
