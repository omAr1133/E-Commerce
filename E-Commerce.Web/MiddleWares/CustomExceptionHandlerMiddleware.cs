using System.Net;
using System.Text.Json;
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType="application/json";
                var response = new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
                //var jsonResult=JsonSerializer.Serialize(response);
                await httpContext.Response.WriteAsJsonAsync(response);

            }


        }
    }
}
