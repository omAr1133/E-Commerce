using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Basket;

namespace Presentation.Controllers
{
    public class BasketsController (IServiceManager serviceManager)
        :APIController
    {
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> Get(string id)
        {
            var basket = await serviceManager.BasketService.GetAsync(id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> Update(BasketDTO basketDTO)
        {
            var basket = await serviceManager.BasketService.UpdateAsync(basketDTO);
            return Ok(basket);
        }

        [HttpDelete]
        public async Task<ActionResult<BasketDTO>> Delete(string id)
        {
            await serviceManager.BasketService.DeleteAsync(id);
            return NoContent(); //204
        }
    }
}
