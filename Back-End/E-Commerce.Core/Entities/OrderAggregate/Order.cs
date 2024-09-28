using Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderAggregate
{
    public  class Order :BaseEntity
    {
        public Order()
        {
            
        }

        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal,string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntenteId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.UtcNow;
        public Address ShippingAddress { get; set; } // this is the address of the buyer

        public DeliveryMethod DeliveryMethod { get; set; }//navigational property [One]
        public ICollection<OrderItem> Items { get; set; } //navigational Propery [Many]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal SubTotal { get; set; }
        public string PaymentIntenteId { get; set; }//navigational Property [One]

        public decimal GetTotal()=> SubTotal+ DeliveryMethod.Cost;






    }
}
