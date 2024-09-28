using AutoMapper;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Entities;
using Test_E_CommerceProject.Dtos;
using Test_E_CommerceProject.Helpers;
using Test_E_CommerceProject.Service.Dtos;
using UserAddress = Talabat.Core.Entities.Identity.Address;
using OrderAddress = Talabat.Core.Entities.OrderAggregate.Address;


namespace Test_E_CommerceProject.Service.Helpers
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {

            CreateMap<Product, ProductToReturnDto>().ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

        
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItems>();
            CreateMap<AddressDto, OrderAddress>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(o=>o.DeliveryMethodCost,o=>o.MapFrom(s=>s.DeliveryMethod.Cost));
            CreateMap<OrderItem,OrderItemDto>()
                .ForMember(d=>d.ProductName,o=>o.MapFrom(s=>s.Product.ProductName))
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());
            CreateMap<UserAddress, AddressDto>()
                .ForMember(d=>d.FirstName,o=>o.MapFrom(s=>s.FName))
                .ForMember(d=>d.LastName,o=>o.MapFrom(s=>s.LName)).ReverseMap();
            ;



        }


    }
}
