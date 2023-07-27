using eShopper.Errors;
using Microsoft.AspNetCore.Mvc;

namespace eShopper.Controllers
{
    [Route("errors/{code}")]
    public class ErrorController : BaseApiController
    {
        [HttpGet]
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
