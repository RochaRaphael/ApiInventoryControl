using Microsoft.AspNetCore.Mvc;

namespace ApiInventoryControl.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeControllers : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok();
        }
               
    }
}
