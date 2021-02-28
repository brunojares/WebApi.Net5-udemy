using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S6A0702.Business;
using S6A0702.Filter;
using S6A0702.Moldel.Entities;
using S6A0702.Util;
using S6A0702.VO;
using S6A0702.VO.v2;
using System;
using System.Net;

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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginOutVO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public ActionResult Signin([FromBody] LoginInVO vo)
        {
            if (vo == null)
                return BadRequest("No inputs");
            try
            {
                var _result = _loginBusiness
                    .Autenticate(vo.UserName, vo.Password)
                    .CreateVO<Token, LoginOutVO>()
                ;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
        [HttpPatch("refresh")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginRefreshOutVO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
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
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
        [HttpPatch("revoke")]
        [Authorize("Bearer")]
        [AuthorizationFilter]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginRevokeOutVO))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public ActionResult Revoke()
        {
            try
            {
                var userName = User.Identity.Name;
                _loginBusiness.RevokeToken(userName);
                return Ok(new LoginRevokeOutVO(userName));
            }
            catch (Exception ex)
            {
                return this.ReturnActionResult(ex);
            }
        }
    }
}
