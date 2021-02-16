using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using S6A0702.Moldel.Entities;
using S6A0702.Business;
using System.Collections.Generic;
using System.Linq;

namespace S5A0504.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private readonly IPersonBusiness _personBusiness;
        public PersonController(
            ILogger<PersonController> logger,
            IPersonBusiness personBusiness
        )
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var _result = _personBusiness.GetAll().ToArray();
            return Ok(_result);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var _result = _personBusiness.GetById(id);
            return _result != null ? Ok(_result) : NotFound();
        }
        [HttpPost]
        public IActionResult Post([FromBody] Person model)
        {
            _personBusiness.Create(ref model);
            return Accepted(model);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person model)
        {
            try
            {
                model.Id = id;
                _personBusiness.Update(ref model);
                return Ok(model);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    title = "Not found",
                    error = new string[]{
                       ex.Message
                    }
                });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personBusiness.DeleteById(id);
            return NoContent();
        }
    }
}
