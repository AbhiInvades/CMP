using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CMP_Server_API.CMP_Server_API.core.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate reqDel)
        {
            _next = reqDel;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context != null)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(ex.ToString());
                    Console.WriteLine(ex.StackTrace);
                    Debug.WriteLine(ex.ToString());
                }
            }


        }

    }
}
