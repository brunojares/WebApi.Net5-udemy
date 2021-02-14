using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using S6A0702.Moldel.Entities;
using S6A0702.Services;
using System.Linq;

namespace S5A0504.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;
        public PersonController(
            ILogger<PersonController> logger, 
            IPersonService personService
        )
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var _result = _personService.GetAll().ToArray();
            return Ok(_result);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var _result = _personService.GetById(id);
            return _result != null ? Ok(_result) : NotFound();
        }
        [HttpPost]
        public IActionResult Post([FromBody] Person model)
        {
            _personService.Create(ref model);
            return Accepted(model);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person model)
        {
            model.Id = id;
            _personService.Update(ref model);
            return Ok(model);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personService.DeleteById(id);
            return NoContent();
        }
    }
}
