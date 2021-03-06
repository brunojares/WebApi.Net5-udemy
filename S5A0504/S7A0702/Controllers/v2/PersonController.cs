﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using S6A0702.Business;
using S6A0702.Filter;
using S6A0702.Moldel.Entities;
using S6A0702.Util;
using S6A0702.VO;
using S6A0702.VO.v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace S5A0504.Controllers.v2
{
    [ApiVersion("2")]
    [ApiExplorerSettings(GroupName = "v2")]
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedCollectionVO<PersonOutVO, Person>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Get(string filter, int currentPage = 1, int pageSize = 10)
        {
            try
            {
                var _entities = _personBusiness.GetByFilter(filter);
                var _result = new PagedCollectionVO<PersonOutVO, Person>(_entities, currentPage, pageSize);
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
        [HttpPatch("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PersonOutVO))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Enabled(int id, bool enabled)
        {
            try
            {
                var _result = _personBusiness
                    .SetEnabled(id, enabled)
                    .CreateVO<Person, PersonOutVO>()
                ;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
    }
}
