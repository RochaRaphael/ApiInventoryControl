using ApiInventoryControl.Data;
using ApiInventoryControl.Extensions;
using ApiInventoryControl.Models;
using ApiInventoryControl.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiInventoryControl.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("v1/product/{name:string}")]
        public async Task<IActionResult> GetAsync(
            [FromRoute] string name,
            [FromServices] InventoryDataContext context
            )
        {
            try
            {
                var products = await context.Products.FirstOrDefaultAsync(x => x.Name == name);
                if (products == null)
                    return NotFound(new ResultViewModel<Product>("Product not found"));
                else
                    return Ok(new ResultViewModel<Product>(products));
            }
            catch
            {
                return StatusCode(500, "05X10 - Internal server failure");
            }
        }



        [HttpPost("v1/product")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Product model,
            [FromServices] InventoryDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

            try
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Category = model.Category
                };
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{product.Id}", new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE9 - Unable to add product"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("05X10 - Internal server failure"));
            }
        }
    }
}
