using Talabat.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_E_CommerceProject.Service.Errors;

namespace Test_E_CommerceProject.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext storeContext;

        public BuggyController(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }
        [HttpGet("notfound")]//https://localhost:5001/api/buggy/notfound
        public ActionResult GetNotFoundRequest()
        {
            var Product = storeContext.Products.Find(42);
            if (Product == null)
            {
                return NotFound(new ApiResponse(404));
                //404
            }
            return Ok(Product);
        }
        [HttpGet("servererror")] // https://localhost:5001/api/buggy/servererror
        public ActionResult GetServerError()
        {
            var Product = storeContext.Products.Find(42);
            var ProductToReturn = Product.ToString();//while throw exception =>null reference exception
            return Ok(ProductToReturn);
        }
        [HttpGet("badrequest")]//https://localhost:5001/api/buggy/badrequest
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{Id}")] // https://localhost:5001/api/buggy/badrequest/five

        public ActionResult GetBadRequest(int Id) //validation error
        {
            return Ok(Id);
        }
        [HttpGet("Unauthorized")]
        public ActionResult GetUnauthorizedError()
        {
            return Unauthorized(new ApiResponse(401));
        }
    }
}
