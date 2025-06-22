 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Orders;

namespace ServicesAbstractions
{
    public interface IOrderService 
    {
        Task<OrderResponse> CreateAsync(OrderRequest orderRequest, string email);
        Task<OrderResponse> GetAsync(Guid id, string email);
        Task<IEnumerable<OrderResponse>> GetAllAsync( string email);
        Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync(); 
    }
}
