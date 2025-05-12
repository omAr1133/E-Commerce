using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModels
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail,  IEnumerable<OrderItem> items, OrderAddress address, DeliveryMethod deliveryMethod,
             decimal subtotal)
        {
            UserEmail = userEmail;
            Items = items;
            Address = address;
            DeliveryMethod = deliveryMethod;

           // PaymentIntentId = paymentIntentId;
            Subtotal = subtotal;
        }

        public string UserEmail { get; set; } = default!;
        public DateTimeOffset Date {  get; set; } = DateTimeOffset.Now;
        public IEnumerable<OrderItem> Items { get; set; } = [];
        public OrderAddress Address { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public string PaymentIntentId { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }

    }
}
