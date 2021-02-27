using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using S6A0702.Business;
using S6A0702.Filter;
using S6A0702.Moldel.Entities;
using S6A0702.VO;
using S6A0702.VO.v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;

namespace S5A0504.Controllers.v2
{
    [ApiVersion("2")]
    [ApiExplorerSettings(GroupName = "v2")]
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

        [HttpPatch("refresh")]
        public ActionResult Refresh([FromBody] LoginRefreshInVO vo)
        {
            if (vo == null)
                return BadRequest("No inputs");
            try
            {
                var _result = _loginBusiness
                    .Refresh(vo.AccessToken, vo.RefreshToken)
                    .CreateVO<Token, LoginRefreshOutVO>()
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
        [HttpPatch("revoke")]
        [Authorize("Bearer")]
        [AuthorizationFilter]
        public ActionResult Revoke()
        {
            try
            {
                var userName = User.Identity.Name;
                _loginBusiness.RevokeToken(userName);
                return Ok(new { userName });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    title = "Not found",
                    erros = new string[]
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
                        erros = new string[]
                        {
                            ex.Message
                        }
                    }
                );
            }
        }
    }
}
