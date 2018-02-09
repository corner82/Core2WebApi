using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Core2WebApi.Extensions;
using Core2WebApi.Core.Hmac;

namespace Core2WebApi.Filters
{
    public class HmacTokenControllerAttribute : ActionFilterAttribute
    {
        private readonly HttpContext _httpContext;
        public HmacTokenControllerAttribute(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }



        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var password = context.HttpContext.GetUserPassword();
            var email = context.HttpContext.GetUserName();



            var token = HmacServiceManager.GenerateToken(user.Email, password
                                                            , _remoteAdresFinder.GetRequestIP()
                                                            , userAgentString
                                                            , ticks);


            base.OnActionExecuting(context);
        }
    }
}
