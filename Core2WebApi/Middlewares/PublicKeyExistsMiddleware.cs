using Core2WebApi.Entities.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core2WebApi.Extensions;

namespace Core2WebApi.Middlewares
{
    public class PublicKeyExistsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _distributedCache;
        public PublicKeyExistsMiddleware(RequestDelegate next,
                                         IDistributedCache distributedCache)
        {
            _next = next;
            _distributedCache = distributedCache;
        }

        public async Task Invoke(HttpContext context)


        {
            context.GetHmacToken();
            if (context.Request.Headers.ContainsKey("X-PublicKey"))
            {
                var publicKey = context.Request.Headers.Where(x => x.Key == "X-PublicKey").FirstOrDefault();
                //if(await _distributedCache.)
                if(!string.IsNullOrEmpty(publicKey.Value))
                {
                    try
                    {
                        var cacheUser = await _distributedCache.GetStringAsync(publicKey.Value);
                        var user = JsonConvert.DeserializeObject<SessionUserModel>(cacheUser);

                        if (user == null)
                        {
                            context.Response.StatusCode = 503;
                            context.Response.ContentType = "application/json";
                            using (var writer = new StreamWriter(context.Response.Body))
                                await writer.WriteLineAsync("{Message : 'User Not Found Due To Public Key'}");
                        }
                        else
                        {
                            context.Items["PublicKey"] = publicKey.Value;
                            var publicKe = context.Items["PublicKey"];
                            await _next(context);
                        }
                    } catch(Exception ex){
                        await context.Response.WriteAsync("redis connection exception");
                    }
                } else
                {
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "application/json";
                    using (var writer = new StreamWriter(context.Response.Body))
                        await writer.WriteLineAsync("{Message : 'Public Key Not Found'}");
                }
            } else
            {
                context.Response.StatusCode = 503;
                context.Response.ContentType = "application/json";
                using (var writer = new StreamWriter(context.Response.Body))
                    await writer.WriteLineAsync("{Message : 'User Not Found Due To Public Key'}");
            }                
            
        }
    }
}
