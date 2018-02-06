using Core2WebApi.Entities.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core2WebApi.Middlewares
{
    public class SetUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _distributedCache;
        private SessionUserModel _sessionUserModel;
        public SetUserMiddleware(RequestDelegate next, 
                                 IDistributedCache distributedCache
                                 )
        {
            _next = next;
            _distributedCache = distributedCache;
            
        }

        public async Task Invoke(HttpContext context, SessionUserModel sessionUserModel)
        {
            _sessionUserModel = sessionUserModel;
            var headerList = context.Request.Headers.ToList();
            var xPublic = headerList.Where(x => x.Key == "X-PublicKey").FirstOrDefault();
            if (!string.IsNullOrEmpty(xPublic.Value))
            {
                var userTestObj = _distributedCache.GetString(xPublic.Value);
                _sessionUserModel = JsonConvert.DeserializeObject<SessionUserModel>(userTestObj);
                var dene = _sessionUserModel.Email;
                context.Items["MiyaUser"] = _sessionUserModel;
                context.Items["PublicKey"] = xPublic.Value;
                var xToken = headerList.Where(x => x.Key == "X-Hmac").FirstOrDefault();
                if (!string.IsNullOrEmpty(xToken.Value))
                {
                    context.Items["Token"] = xToken.Value;
                }
                
                //await _next(context);
            } else
            {
                //await _next(context);
            }
            await _next(context);
            //await context.Response.WriteAsync(_greeter.Greet());
           
        }
    }
}
