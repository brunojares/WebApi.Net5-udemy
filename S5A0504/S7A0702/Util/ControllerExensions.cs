using Microsoft.AspNetCore.Mvc;
using S6A0702.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security;

namespace S6A0702.Util
{
    public static class ControllerExensions
    {
        public static ActionResult ReturnActionResult(this ControllerBase controller, Exception exception)
        {
            switch (exception.GetType().Name)
            {
                case nameof(FileNotFoundException):
                case nameof(KeyNotFoundException): return controller.NotFound(new ErrorVO("Not found", exception.Message));
                case nameof(ArgumentNullException): return controller.BadRequest(new ErrorVO("Invalid input", exception.Message));
                case nameof(SecurityException): return controller.Unauthorized(new ErrorVO("Access denied", exception.Message));
                default:
                    {
                        return controller.StatusCode(
                            (int)HttpStatusCode.InternalServerError,
                            new ErrorVO("Service error", exception.Message)
                        );
                    }
            }
        }
        public static string ReturnApiGroupName(this ControllerBase controllerBase)
        {
            var _attribute = controllerBase.GetType().GetCustomAttribute<ApiExplorerSettingsAttribute>();
            return _attribute.GroupName;
        }
    }
}
