using CMP_Server_API.CMP_Server_API.core.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;


namespace CMP_Server_API.CMP_Server_API.core.Extensions
{
    public static class GlobalExceptionHandler
    {

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder =>
                   builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            }
            else
            {
                //will not give full stack trace, if environment is non dev, for security reasons.
                //app.UseExceptionHandler(app =>
                //{
                //    app.Run(async context =>
                //    {
                //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //        context.Response.ContentType= "application/json";
                //        await context.Response.WriteAsync("Internal error");

                //    });
                //});

                //In order to send message specific to specific exceptions, custom middleware must be used with app.usemiddleware<>()

                app.UseMiddleware<ExceptionMiddleware>();
            }
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

}
