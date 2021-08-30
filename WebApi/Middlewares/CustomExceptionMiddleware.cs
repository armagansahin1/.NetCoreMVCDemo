using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;
        public CustomExceptionMiddleWare(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {   
            var watch = Stopwatch.StartNew();
            try
            {
            string message = "[Request] HTTP :" + context.Request.Path + " - " + context.Request.Method;
            _logger.Write(message);
            await _next(context);
            watch.Stop();
            message = "[Response] HTTP :" + context.Request.Path + " - " + context.Request.Method + " - " + context.Response.StatusCode + " - in " +
            watch.ElapsedMilliseconds + " ms";
            _logger.Write(message);
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
            _logger.Write(message);
            

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