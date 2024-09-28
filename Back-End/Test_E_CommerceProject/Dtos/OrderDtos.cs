using Talabat.Core.Entities.OrderAggregate;
using System.ComponentModel.DataAnnotations;

namespace Test_E_CommerceProject.Dtos
{
    public class OrderDtos
    {

        [Required]
        public string BasketId { get; set; }
        [Required]

        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }





    }
}
