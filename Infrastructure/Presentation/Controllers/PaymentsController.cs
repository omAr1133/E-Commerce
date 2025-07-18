using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Basket;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager service)
        : APIController
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdate(string basketId)
        {
            return Ok(await service.PaymentService.CreateOrUpdatepaymentIntent(basketId));
        }

    }
}
