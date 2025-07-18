using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class OrderWithPaymentIntentSpecification(string paymentIntentId)
        :BaseSpecifications<Order>(Order => Order.PaymentIntentId == paymentIntentId)
    {
    }
}
