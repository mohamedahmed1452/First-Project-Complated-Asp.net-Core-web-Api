using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_E_CommerceProject.Api.Controllers;
using Test_E_CommerceProject.Service.Errors;

namespace Test_E_CommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [ProducesResponseType(typeof(CustomerBasket),200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("{basketId}")] //api/payments/basketId
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket is null) return BadRequest(new ApiResponse(400,"An Error With Your Basket"));
            return Ok(basket);
        }
    }
}
