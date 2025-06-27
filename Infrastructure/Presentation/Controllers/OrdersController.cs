using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Orders;

namespace Presentation.Controllers
{
    public class OrdersController(IServiceManager service)
        : APIController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Create(OrderRequest request)
        {
            return Ok(await service.OrderService.CreateAsync(request, GetEmailFromToken()));
        }
    }
}
