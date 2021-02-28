using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading.Tasks;

namespace S6A0702.Util
{
    public static class ControllerExensions
    {
        public static ActionResult ReturnActionResult(this ControllerBase controller, Exception exception)
        {
            switch (exception.GetType().Name)
            {
                case nameof(KeyNotFoundException):
                    {
                        return controller.NotFound(new
                        {
                            title = "Not found",
                            errors = new string[] { exception.Message }
                        });
                    }
                case nameof(SecurityException):
                    {
                        return controller.Unauthorized(new
                        {
                            title = "Access denied",
                            errors = new string[] { exception.Message }
                        });
                    }
                default:
                    {
                        return controller.StatusCode(
                            (int)HttpStatusCode.InternalServerError,
                            new
                            {
                                title = "Service error",
                                errors = new string[] { exception.Message }
                            }
                        );
                    }
            }
        }
    }
}
