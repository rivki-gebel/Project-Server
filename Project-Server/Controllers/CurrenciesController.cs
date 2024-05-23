using Microsoft.AspNetCore.Mvc;
using Project_Server.Entities;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_Server.Controllers
{
        
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly Service _service;

        public CurrenciesController(Service service)
        {
            _service = service;
        }

        // GET: api/<CurrenciesController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            List<Currency> list=await _service.GetCurrenciesList();
            if (list == null)
                return BadRequest();
            return Ok(list);
        }


    }
}
