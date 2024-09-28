using AutoMapper;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Services.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Test_E_CommerceProject.Api.Controllers;
using Test_E_CommerceProject.Dtos;
using Test_E_CommerceProject.Service.Errors;

namespace Test_E_CommerceProject.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }



        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDtos orderDtos)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var address=_mapper.Map<AddressDto,Address>(orderDtos.ShippingAddress);
            var order=await _orderService.CreateOrderAsync(buyerEmail, orderDtos.DeliveryMethodId, orderDtos.BasketId, address);
            if (order is null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order,OrderToReturnDto>(order));
        
        }



        [HttpGet]// api/orders?email=mohamed_ahmed@gmail.com
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders=await _orderService.GetOrdersForUserAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));

        }



        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{Id}")]// api/orders/1?email=mohamed_ahmed@gmail.com
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int Id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order=await _orderService.GetOrderByIdForUserAsync(Id, email);
            if(order is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }





        [HttpGet("deliveryMethods")]
        public async Task<ActionResult< IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}
