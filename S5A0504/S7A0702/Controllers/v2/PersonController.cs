﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using S6A0702.Business;
using S6A0702.Moldel.Entities;
using S6A0702.VO;
using S6A0702.VO.v2;
using System.Collections.Generic;
using System.Linq;

namespace S5A0504.Controllers.v2
{
    [ApiVersion("2")]
    [ApiExplorerSettings(GroupName = "v2")]
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
            var _result = _personBusiness
                .GetAll()
                .Parse<Person, PersonVO>()
                .ToArray()
            ;
            return Ok(_result);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var _result = _personBusiness
                .GetById(id)
                .CreateVO<Person, PersonVO>()
            ;
            return
                _result != null ?
                Ok(_result) :
                NotFound(new
                {
                    title = "Not found",
                    error = new string[]{
                       $"Person {id} not found"
                    }
                })
            ;
        }
        [HttpPost]
        public IActionResult Post([FromBody] PersonVO model)
        {
            var _entity = model.CreateEntity();
            _personBusiness.Create(ref _entity);
            return Accepted(_entity);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PersonVO model)
        {
            try
            {
                var _entity = model.CreateEntity();
                _entity.Id = id;
                _personBusiness.Update(ref _entity);
                return Ok(_entity);
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
