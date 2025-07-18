using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Basket;

namespace ServicesAbstractions
{
    public interface IPaymentService
    {
        Task<BasketDTO> CreateOrUpdatepaymentIntent(string basketId);
    }
}
