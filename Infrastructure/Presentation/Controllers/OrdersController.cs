using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Orders;

namespace Presentation.Controllers

{
    [Authorize]
    public class OrdersController(IServiceManager service)
        : APIController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Create(OrderRequest request)
        {
            return Ok(await service.OrderService.CreateAsync(request, GetEmailFromToken()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAll()
        {
            return Ok(await service.OrderService.GetAllAsync(GetEmailFromToken()));
        }

        [HttpGet ("{id:guid}")]
        public async Task<ActionResult<OrderResponse>> Get(Guid id)
        {
            return Ok(await service.OrderService.GetAsync(id));
        }

        [HttpGet("deliveryMethods"),AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResponse>>> GetDeliveryMethods()
        {
            return Ok(await service.OrderService.GetDeliveryMethodsAsync());
        }

    }
}
