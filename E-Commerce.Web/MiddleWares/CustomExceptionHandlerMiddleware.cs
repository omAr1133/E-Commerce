using System.Net;
using System.Text.Json;
using Domain.Exceptions;
using Shared.ErrorModels;

namespace E_Commerce.Web.MiddleWares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next; 
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);

                await HandleNotFoundEndPointAsync(httpContext);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                //httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                await HandleExceptionAsync(httpContext, ex);

            }


        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                ErrorMessage = ex.Message
            };
            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError 
            };
            //var jsonResult=JsonSerializer.Serialize(response);

            httpContext.Response.StatusCode = response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandleNotFoundPathAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                httpContext.Response.ContentType = "application/json";
                var response = new ErrorDetails
                {
                    ErrorMessage = $"Path {httpContext.Request.Path} NotFound",
                    StatusCode = StatusCodes.Status404NotFound
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }



    }
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            return app;
        }
    }
}
