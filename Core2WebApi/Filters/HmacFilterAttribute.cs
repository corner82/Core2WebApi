using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2WebApi.Filters
{
    public class HmacFilterAttribute : ActionFilterAttribute
    {

        public HmacFilterAttribute()
        {

        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context!=null)
            {
                if(!context.HttpContext.Request.Headers.ContainsKey("X-Hmac"))
                {
                    context.Result = new StatusCodeResult(405);
                }
            }
        }
    }
}
