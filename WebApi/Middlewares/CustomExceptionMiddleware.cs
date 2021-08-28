using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleWare
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {   
            var watch = Stopwatch.StartNew();
            
            

            try
            {
            string message = "[Request] HTTP :" + context.Request.Path + " - " + context.Request.Method;
            Console.WriteLine(message);
            await _next(context);
            watch.Stop();
            message = "[Response] HTTP :" + context.Request.Path + " - " + context.Request.Method + " - " + context.Response.StatusCode + " - in " +
            watch.ElapsedMilliseconds + " ms";
            Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                
                watch.Stop();
                await HandleException(watch,context,ex);
            }

        }

        private Task HandleException(Stopwatch watch, HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var message = "[Error] HTTP :" + context.Request.Path + " - " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message : "+ ex.Message+ " - in " +
            watch.ElapsedMilliseconds + " ms";
            Console.WriteLine(message);
            

            var result = JsonConvert.SerializeObject(new {error = ex.Message}, Formatting.None);
            return context.Response.WriteAsync(result);
        }

    }
    public static class CustomExceptionMiddleWareExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleWare>();
        }
    }
}