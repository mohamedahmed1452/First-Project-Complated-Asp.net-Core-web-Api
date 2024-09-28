using Talabat.Core.Entities;

namespace Test_E_CommerceProject.Service.Dtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
