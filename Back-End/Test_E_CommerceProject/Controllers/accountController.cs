using AutoMapper;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Test_E_CommerceProject.Api.Controllers;
using Test_E_CommerceProject.Dtos;
using Test_E_CommerceProject.Extensions;
using Test_E_CommerceProject.Service.Dtos;
using Test_E_CommerceProject.Service.Errors;

namespace Test_E_CommerceProject.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class accountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public accountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IAuthService authService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("login")]// 
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)

            });
         
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (checkEmailExist(model.Email).Result.Value)
            {
                return Ok(new ApiValidationErrorResponse() { Errors = new string[] { "this email is already exist in user!!" } });
            }


            var user= new AppUser
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0].Replace(".", "").Replace("_", ""),
                PhoneNumber=model.PhoneNumber
            };
            var result=await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded is false)
            {
                return BadRequest(new ApiResponse(400));
            }
            else
            {
                return Ok(new UserDto
                {
                    DisplayName= user.DisplayName,
                    Email = user.Email,
                    Token = await _authService.CreateTokenAsync(user, _userManager)
                });
            }
         
        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet] // api/account
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {

            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            };
        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("address")] // api/account/address
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            var address = _mapper.Map<Address,AddressDto>(user.Address);
            return Ok(address);
     
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("address")] // api/account/address
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto updatedAddress)
        {
            var address = _mapper.Map<AddressDto, Address>(updatedAddress);
            var user = await _userManager.FindUserWithAddressAsync(User);
            address.Id = user.Address.Id;
            user.Address = address;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(updatedAddress);
            return BadRequest(new ApiResponse(400));
        }


        [HttpGet("emailexists")] // api/account/emailexists?email=mohamed_ahmed@gmail.com
        public async Task<ActionResult<bool>> checkEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
