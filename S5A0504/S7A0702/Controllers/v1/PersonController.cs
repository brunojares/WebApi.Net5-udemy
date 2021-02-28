using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using S6A0702.Business;
using S6A0702.Filter;
using S6A0702.Moldel.Entities;
using S6A0702.Util;
using S6A0702.VO;
using S6A0702.VO.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace S5A0504.Controllers.v1
{
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [Authorize("Bearer")]
    [AuthorizationFilter]
    [Route("v{version:apiVersion}/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonBusiness _personBusiness;
        public PersonController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        [HttpGet]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PersonOutVO[]))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Get()
        {
            try
            {
                var _result = _personBusiness
                    .GetAll()
                    .Parse<Person, PersonOutVO>()
                    .ToArray()
                ;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PersonOutVO))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Get(int id)
        {
            try
            {
                var _result =
                    _personBusiness.GetById(id).CreateVO<Person, PersonOutVO>() ??
                    throw new KeyNotFoundException($"Person {id} not found")
                ;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted, Type = typeof(PersonOutVO))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Post([FromBody] PersonInVO model)
        {
            try
            {
                var _entity = model.CreateEntity();
                _personBusiness.Create(ref _entity);
                return Accepted(_entity);
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PersonOutVO))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Put(int id, [FromBody] PersonInVO model)
        {
            try
            {
                var _entity = model.CreateEntity();
                _entity.Id = id;
                _personBusiness.Update(ref _entity);
                return Ok(_entity);
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Delete(int id)
        {
            try
            {
                _personBusiness.DeleteById(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
    }
}
