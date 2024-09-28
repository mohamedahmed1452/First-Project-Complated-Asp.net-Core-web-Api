using Talabat.Core.Entities.OrderAggregate;

namespace Test_E_CommerceProject.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } 
        public Address ShippingAddress { get; set; } // this is the address of the buyer
        public string Status { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();//navigational Propery [Many]


        public int PaymentIntentId { get; set; }//navigational Property [One]


    }
}
