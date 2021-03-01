using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S6A0702.Business;
using S6A0702.Filter;
using S6A0702.Util;
using S6A0702.VO;
using S6A0702.VO.v2;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace S6A0702.Controllers.v2
{
    [ApiVersion("2")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    [Authorize("Bearer")]
    [AuthorizationFilter]
    [Route("v{version:apiVersion}/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpGet("{fileName}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(byte[]))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorVO))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorVO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorVO))]
        [Produces("application/octet-stream")]
        public ActionResult Get(string fileName)
        {
            try
            {
                var _content = _fileBusiness.GetFile(fileName);
                return File(_content, "application/octet-stream");
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted, Type = typeof(FileOutVO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorVO))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorVO))]
        public async Task<IActionResult> Post([FromForm] IFormFile file)
        {
            try
            {
                var _apiVersion = this.ReturnApiGroupName();
                var _result = await _fileBusiness.SaveFile(file, _apiVersion);
                return new OkObjectResult(_result);
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
        [HttpPost("Many")]
        [ProducesResponseType((int)HttpStatusCode.Accepted, Type = typeof(FileOutVO[]))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorVO))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorVO))]
        public async Task<IActionResult> Post([FromForm] List<IFormFile> files)
        {
            try
            {
                var _apiVersion = this.ReturnApiGroupName();
                var _result = await _fileBusiness.SaveFiles(files, _apiVersion);
                return new OkObjectResult(_result);
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
    }
}
