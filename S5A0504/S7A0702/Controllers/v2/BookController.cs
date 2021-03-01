using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S6A0702.Business;
using S6A0702.Filter;
using S6A0702.Moldel.Entities;
using S6A0702.Util;
using S6A0702.VO;
using S6A0702.VO.v2;
using System;
using System.Collections.Generic;
using System.Net;

namespace S6A0702.Controllers.v2
{
    [ApiVersion("2")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    [Authorize("Bearer")]
    [AuthorizationFilter]
    [Route("v{version:apiVersion}/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness _bookBusiness;

        public BookController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedCollectionVO<BookOutVO, Book>))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorVO))]
        public ActionResult Get(string filter, int currentPage = 1, int pageSize = 10)
        {
            try
            {
                var _entities = _bookBusiness.GetByFilter(filter);
                var _result = new PagedCollectionVO<BookOutVO, Book>(_entities, currentPage, pageSize);
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BookOutVO))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorVO))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorVO))]
        public ActionResult Get(long id)
        {
            try
            {
                var _result =
                    _bookBusiness.GetById(id).CreateVO<Book, BookOutVO>() ??
                    throw new KeyNotFoundException($"Book {id} not found")
                ;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted, Type = typeof(BookOutVO[]))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorVO))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorVO))]
        public ActionResult Post([FromBody] BookInVO book)
        {
            try
            {
                var _entity = book.CreateEntity();
                _bookBusiness.Create(ref _entity);
                var _result = _entity.CreateVO<Book, BookOutVO>();
                return Accepted(_result);
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BookOutVO[]))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorVO))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorVO))]
        public ActionResult Put(long id, [FromBody] BookInVO book)
        {
            try
            {
                var _entity = book.CreateEntity();
                _entity.Id = id;
                _bookBusiness.Update(ref _entity);
                var _result = _entity.CreateVO<Book, BookOutVO>();
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent, Type = typeof(ErrorVO))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorVO))]
        public ActionResult Delete(long id)
        {
            try
            {
                _bookBusiness.DeleteById(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
    }
}