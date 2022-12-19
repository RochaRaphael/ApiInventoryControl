using ApiInventoryControl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiInventoryControl.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("v1/Product/{name:string}")]
        public async Task<IActionResult> GetAsync(
            [FromRoute] string name,
            [FromServices] InventoryDataContext context
            )
        {
            try
            {
                var products = await context.Products.FirstOrDefaultAsync(x => x.Name == name);
                if (products == null)
                    return NotFound("Product not found");

                return Ok(products);
            }
            catch
            {
                return StatusCode(500, "05X10 - Internal server failure");
            }
        }
    }
}
