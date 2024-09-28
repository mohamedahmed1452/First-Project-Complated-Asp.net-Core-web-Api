using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_E_CommerceProject.Api.Controllers;
using Test_E_CommerceProject.Service.Dtos;
using Test_E_CommerceProject.Service.Errors;

namespace Test_E_CommerceProject.Service.Controllers
{

    public class BasketController : BaseApiController
    {

        private readonly IBasketRepository basketrepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketrepository, IMapper mapper)
        {
            this.basketrepository = basketrepository;
            _mapper = mapper;
        }


        [HttpGet]// api/basket/{Id}
        public async Task<ActionResult<CustomerBasket>> GetBasket(string Id)
        {
            var basket = await basketrepository.GetBasketAsync(Id);
            return Ok(basket ?? new CustomerBasket(Id));
        }

        [HttpPost]// 
        public async Task<ActionResult<CustomerBasket>> UpdateCustomer(CustomerBasketDto customerBasket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasket);
            var createdOrUpdatedBasket = await basketrepository.UpdateBasketAsync(mappedBasket);

            return createdOrUpdatedBasket is not null ? Ok(createdOrUpdatedBasket) : Ok(BadRequest(new ApiResponse(400)));
        }
        [HttpDelete] // api/basket/{Id}
        public async Task DeleteBasket(string Id)
        {
            await basketrepository.DeleteBasketAsync(Id);
        }
    }
}
