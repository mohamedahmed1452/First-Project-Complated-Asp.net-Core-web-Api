using AutoMapper;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Entities;
using Test_E_CommerceProject.Dtos;
using Test_E_CommerceProject.Service.Dtos;

namespace Test_E_CommerceProject.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
   

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return $"{configuration["ApiBaseUrl"]}/{source.Product.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
