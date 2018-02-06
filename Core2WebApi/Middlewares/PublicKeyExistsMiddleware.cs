using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core2WebApi.Middlewares
{
    public class PublicKeyExistsMiddleware
    {
        private readonly RequestDelegate _next;
        public PublicKeyExistsMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("X-Public"))
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "application/json";
                using (var writer = new StreamWriter(context.Response.Body))
                    await writer.WriteLineAsync("{Message : 'Public Key Not Found'}");
            }
                                    
            //await _next(context);


        }
    }
}
