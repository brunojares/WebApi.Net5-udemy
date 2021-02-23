using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using S6A0702.Business;
using S6A0702.Moldel.Entities;
using S6A0702.VO;
using S6A0702.VO.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;

namespace S5A0504.Controllers.v1
{
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class LoginController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public LoginController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost("signin")]
        public ActionResult Signin([FromBody] LoginInVO vo)
        {
            if (vo == null)
                return BadRequest("No credentials");
            try
            {
                var _result = _loginBusiness
                    .Autenticate(vo.UserName, vo.Password)
                    .CreateVO<Token, LoginOutVO>()
                ;
                return Ok(_result);
            }
            catch (SecurityException ex)
            {
                return Unauthorized(new
                {
                    title = "Access denied",
                    errors = new string[]
                    {
                        ex.Message
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new
                    {
                        title = "Service error",
                        errors = new string[]
                        {
                            ex.Message
                        }
                    }
                );
            }
        }
    }
}
