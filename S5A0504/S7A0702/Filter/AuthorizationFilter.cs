using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using S6A0702.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
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
                context.Result = _controller.Unauthorized(new
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
                context.Result = _controller.StatusCode(
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
