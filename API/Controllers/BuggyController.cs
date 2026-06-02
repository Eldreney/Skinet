using Api.Controllers;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  
   

    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : BaseApiController
    {
      
    [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("Not A Good Request");
        }

        [HttpGet("get-not-found")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }


        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            throw new Exception("This is a server error");
        }

        [HttpPost("validation-error")]
        public IActionResult GetValidationError(Product product)
        {
            return Ok();
        }







    }
}