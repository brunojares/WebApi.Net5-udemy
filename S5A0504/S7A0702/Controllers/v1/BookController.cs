using Microsoft.AspNetCore.Mvc;
using S6A0702.Business;
using S6A0702.Moldel.Entities;
using S6A0702.VO;
using S6A0702.VO.v1;
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
            var _result = _bookBusiness
                .GetAll()
                .Parse<Book, BookOutVO>()
                .ToArray()
            ;
            return Ok(_result);
        }
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var _result = _bookBusiness
                .GetById(id)
                .CreateVO<Book, BookOutVO>()
            ;
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
        public ActionResult Post([FromBody] BookInVO book)
        {
            var _entity = book.CreateEntity();
            _bookBusiness.Create(ref _entity);
            return Accepted(_entity);
        }
        [HttpPut("{id}")]
        public ActionResult Put(long id, [FromBody] BookInVO book)
        {
            try
            {
                var _entity = book.CreateEntity();
                _entity.Id = id;
                _bookBusiness.Update(ref _entity);
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
        public ActionResult Delete(long id)
        {
            _bookBusiness.DeleteById(id);
            return NoContent();
        }
    }
}
