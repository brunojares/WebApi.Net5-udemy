using Microsoft.AspNetCore.Mvc;
using S6A0702.Business;
using S6A0702.Moldel.Entities;
using System.Collections.Generic;
using System.Linq;

namespace S6A0702.Controllers.v1
{
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness _bookBusiness;

        public BookController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var _result = _bookBusiness.GetAll().ToArray();
            return Ok(_result);
        }
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var _result = _bookBusiness.GetById(id);
            return
                _result != null ?
                Ok(_result) :
                NotFound(new
                {
                    title = "Not Found",
                    error = new string[]
                    {
                        $"Book {id} not found"
                    }
                })
            ;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            _bookBusiness.Create(ref book);
            return Accepted(book);
        }
        [HttpPut("{id}")]
        public ActionResult Put(long id, [FromBody] Book book)
        {
            try
            {
                book.Id = id;
                _bookBusiness.Update(ref book);
                return Ok(book);
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
        public ActionResult Delete(long id)
        {
            _bookBusiness.DeleteById(id);
            return NoContent();
        }
    }
}
