using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Authentication;

namespace Shared.Orders
{
    public record OrderResponse
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset Date { get; set; }
        public IEnumerable<OrderItemDTO> Items { get; set; } = [];
        public AddressDTO Address { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public string PaymentStatus { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }

    public record OrderItemDTO 
    {
        public string PictureUrl { get; set; }
        public string ProductName { get; set; } 
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
