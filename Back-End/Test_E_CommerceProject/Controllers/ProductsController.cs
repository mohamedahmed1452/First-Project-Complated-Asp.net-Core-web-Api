using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.ProductSpec;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specification;
using Talabat.Core.Specification.Product_Specs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Service;
using Test_E_CommerceProject.Service.Dtos;
using Test_E_CommerceProject.Service.Errors;
using Test_E_CommerceProject.Service.Helpers;

namespace Test_E_CommerceProject.Api.Controllers
{

    public class ProductsController : BaseApiController
    {
   
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService,IMapper mapper)//i want allow Dependancy injection to inject IGenericRepository<Product> ProductsRepo
        {
            _productService = productService;
            _mapper=mapper;
        }

        #region Without Specification
        //[HttpGet]
        //public async Task<IEnumerable<Product>> GetProducts()
        //{
        //    var products = await productsRepo.GetAllAsync();
        //    return products;
        //}
        //[HttpGet("{Id}")]
        //public async Task<IActionResult> GetProduct(int Id)
        //{

        //    var product = await productsRepo.GetAsync(Id);

        //    return product != null ? Ok(product) : NotFound(new { messsage = "This Product is Not Found", StatusCode = 404 });
        //}
        #endregion

        #region With Specification
        //[HttpGet]
        //public async Task<IEnumerable<Product>> GetProducts()
        //{
        //    var spec = new ProductWithBrandAndTypeSpecification();
        //    var products = await productsRepo.GetAllWithSpecAsync(spec);
        //    return products;
        //}
        //[HttpGet("{Id}")]
        //public async Task<ActionResult<Product>> GetProduct(int Id)
        //{
        //    var spec = new ProductWithBrandAndTypeSpecification(Id);
        //    var product = await productsRepo.GetWithSpecAsync(spec);

        //    return product != null ? Ok(product) : NotFound(new { messsage = "This Product is Not Found", StatusCode = 404 });
        //}
        #endregion





        #region EndPoint Using Mapper =>View 
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams? _params)
        {
        
            var products =await _productService.GetProductAsync(_params);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var specCount = new ProductsWithFilterationForCountSpecification(_params);
            int Count = await _productService.GetCountAsync(_params);
            return Ok(new Pagination<ProductToReturnDto>(_params.PageIndex, _params.PageSize, Count, data));

        }
        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int Id)
        {
         
            var product = await _productService.GetProductAsync(Id);

            return product != null ? Ok(_mapper.Map<Product, ProductToReturnDto>(product)) : NotFound(new ApiResponse(404));
        }

        [HttpGet("Categories")]//api/product/catagories
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetProductCategories()
        {
            var types = await _productService.GetCategoriesAsync();
            return Ok(types);
        }
        [HttpGet("brands")] //api/product/Brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productService.GetBrandsAsync();
            return Ok(brands);
        }
        #endregion




    }
}
