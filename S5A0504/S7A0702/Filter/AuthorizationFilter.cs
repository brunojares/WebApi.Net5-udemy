using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using S6A0702.Business;
using S6A0702.VO;
using System;
using System.Net;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace S6A0702.Filter
{
    /// <summary>
    /// https://docs.microsoft.com/pt-br/aspnet/web-api/overview/security/authentication-filters
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _controller = context.Controller as ControllerBase;
            try
            {
                var _loginBusiness =
                    context.HttpContext.RequestServices.
                    GetService(typeof(ILoginBusiness)) as ILoginBusiness
                ;

                _loginBusiness.Authorize(_controller.User.Identity.Name);
            }
            catch (SecurityException ex)
            {
                context.Result = _controller.Unauthorized(new ErrorVO("Access denied", ex.Message));
            }
            catch (Exception ex)
            {
                context.Result = _controller.StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ErrorVO("Service error", ex.Message)
                );
            }
        }
    }
}