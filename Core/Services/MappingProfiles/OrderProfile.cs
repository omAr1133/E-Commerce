using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using Shared.Orders;

namespace Services.MappingProfiles
{
    internal class OrderProfile : Profile
    {
        public OrderProfile() 
        { 
            CreateMap<OrderAddress,AddressDTO>();
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductName,
                o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl,
                o => o.MapFrom<OrderItemPictureUrlResolver>());
            CreateMap<Order, OrderResponse>()
                .ForMember(d => d.DeliveryMethod,
                o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.Total,
                o => o.MapFrom(s => s.DeliveryMethod.Price+s.Subtotal));
        }
    }

    internal class OrderItemPictureUrlResolver(IConfiguration configuration)
        : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.Product.PictureUrl))
            {
                return $"{configuration["BaseUrl"]}{source.Product.PictureUrl}";
            }
            return "";
        }
    }
}
