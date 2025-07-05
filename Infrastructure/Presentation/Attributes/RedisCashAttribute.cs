using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Attributes
{
    internal class RedisCashAttribute(int durationInSec = 90)
    //: Attribute, IAsyncActionFilter
    : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var service = context.HttpContext.RequestServices.GetRequiredService<ICashService>();
            string cashkey = CreateCashKey(context.HttpContext.Request);
            var cashValue = await service.GetAsync(cashkey);

            if(cashValue != null)
            {
                context.Result = new ContentResult
                {
                    Content = cashValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var excutedContext = await next.Invoke();
            if(excutedContext.Result is OkObjectResult result)
            {
                await service.SetAsync(cashkey, result.Value, TimeSpan.FromSeconds(durationInSec));
            }
        }

        private string CreateCashKey(HttpRequest request)
        {
            StringBuilder builder = new();
            builder.Append(request.Path + '?');
            foreach(var item in request.Query.OrderBy(q => q.Key))
                builder.Append($"{item.Key}={item.Value}&");
            return builder.ToString().Trim('&');
        }
    }
}
