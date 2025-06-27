using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModels;
using Shared.Orders;

namespace Services
{
    internal class OrderService (IMapper mapper,IUnitOfWork _unitOfWork, IBasketRepository _basketRepository)
        : IOrderService

    {
        public async Task<OrderResponse> CreateAsync(OrderRequest request, string email)
        {
            var basket = await _basketRepository.GetAsync(request.BasketId) ??
                throw new BasketNotFoundException(request.BasketId);

            List<OrderItem> items = [];
            var productRepo = _unitOfWork.GetRepository<Product>();
            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                items.Add(CreateOrderItem(product , item));
                item.Price = product.Price;
            }
            var address = mapper.Map<OrderAddress>(request.Address);

            var method = await _unitOfWork.GetRepository<DeliveryMethod>()
                .GetAsync(request.DeliveryMethodId)
                ??throw new DeliveryNotFoundException(request.DeliveryMethodId);

            var subtotal = items.Sum(x=>x.Quantity * x.Price);
            var order = new Order(email, items, address, method, subtotal);
            _unitOfWork.GetRepository<Order,Guid>()
                .Add(order);
            await _unitOfWork.SaveChangesAsync();

            return mapper.Map<OrderResponse>(order);
        }

        private static OrderItem CreateOrderItem(Product product, BasketItem item)
        => new(new(product.Id,product.Name,product.PictureUrl), product.Price, item.Quantity);

        public Task<IEnumerable<OrderResponse>> GetAllAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponse> GetAsync(Guid id, string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
