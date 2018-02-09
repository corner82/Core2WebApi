using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Core2WebApi.Extensions;
using Core2WebApi.Core.Hmac;
using Core2WebApi.Core.Utills;
using Wangkanai.Detection;
using System;

namespace Core2WebApi.Filters
{
    public class HmacTokenControllerAttribute : ActionFilterAttribute
    {
        private readonly RemoteAddressFinder _remoteAdressFinder;
        private readonly IDeviceResolver _deviceResolver;
        public HmacTokenControllerAttribute(RemoteAddressFinder remoteAdressFinder,
                                            IDeviceResolver deviceResolver)
        {
            _remoteAdressFinder = remoteAdressFinder;
            _deviceResolver = deviceResolver;
        }



        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var password = context.HttpContext.GetUserPassword();
            var privateKey = context.HttpContext.GetPrivateKey();
            var email = context.HttpContext.GetUserName();
            var userName = context.HttpContext.GetUserName();
            var ip = _remoteAdressFinder.GetRequestIP();
            //var userAgentString = _deviceResolver.UserAgent.ToString();
            var token = context.HttpContext.GetHmacToken();
            var userAgentString = context.HttpContext.GetUserAgent();
            if (!HmacServiceManager.IsTokenValid(userName, privateKey, token, ip, userAgentString))
            {
                context.HttpContext.Response.StatusCode = 403;
                context.HttpContext.Response.ContentType = "Application/Json";
                context.HttpContext.Response.WriteAsync("Invalid Token");
            }
            base.OnActionExecuting(context);
        }
    }
}
