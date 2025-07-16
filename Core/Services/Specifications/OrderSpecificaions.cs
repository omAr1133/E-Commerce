using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Specifications
{
    internal class OrderSpecificaions
        : BaseSpecifications<Order>
    {
        public OrderSpecificaions(Guid id ) 
            : base(order=>order.Id == id)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);
        }
        public OrderSpecificaions(string email)
            : base(order =>order.BuyerEmail == email )
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);
            AddOrderByDescending(x => x.OrderDate);
        }

    }
}
