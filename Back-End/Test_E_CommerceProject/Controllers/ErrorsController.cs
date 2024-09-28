using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_E_CommerceProject.Service.Errors;

namespace Test_E_CommerceProject.Service.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code, "EndPoint Not Found"));
        }
    }
}
