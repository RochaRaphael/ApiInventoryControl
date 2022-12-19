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
            [FromBody] EditorProductViewModel model,
            [FromServices] InventoryDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Product>(ModelState.GetErrors()));

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
                return StatusCode(500, new ResultViewModel<Product>("05XE9 - Unable to add product"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Product>("05X10 - Internal server failure"));
            }
        }


        [HttpPut("v1/product/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorProductViewModel model,
            [FromServices] InventoryDataContext context)
        {
            try
            {
                var product = await context
                    .Products
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return NotFound(new ResultViewModel<Product>("Product not found"));

                product.Name = model.Name;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.Category = model.Category;

                context.Products.Update(product);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05XE8 - Unable to change the product"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05X11 - Internal server failure"));
            }
        }



        [HttpDelete("v1/product/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] InventoryDataContext context)
        {
            try
            {
                var product = await context
                    .Products
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return NotFound(new ResultViewModel<Product>("Product not found"));

                context.Products.Remove(product);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05XE7 - Unable to delete product"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05X12 - Internal server failure"));
            }
        }


    }
}
