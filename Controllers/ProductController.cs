using ApiInventoryControl.Data;
using ApiInventoryControl.Extensions;
using ApiInventoryControl.Models;
using ApiInventoryControl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiInventoryControl.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("v1/product/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] InventoryDataContext context)
        {
            try
            {
                var product = await context
                    .Products
                    .AsNoTracking()
                    .Include(x =>x.Category)
                    .FirstOrDefaultAsync(x => x.Id == id);
                
                if(product == null)
                    return NotFound(new ResultViewModel<Product>("Product not found"));

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("02X14 - Internal server failure" + ex.Message));
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
                Product newProduct;
                var category = await context
                    .Categories
                    .FirstOrDefaultAsync(x => x.Name == model.Category);

                if (category == null)
                {
                    newProduct = new Product
                    {
                        Name = model.Name,
                        Price = model.Price,
                        Quantity = model.Quantity,
                        Category = new Category { Name = model.Category }
                    };
                }
                else
                {
                    newProduct = new Product
                    {
                        Name = model.Name,
                        Price = model.Price,
                        Quantity = model.Quantity,
                        Category = category
                    };
                }
                await context.Products.AddAsync(newProduct);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{newProduct.Id}", new ResultViewModel<Product>(newProduct));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Product>("05XE9 - Unable to add product"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Product>("05X10 - Internal server failure"));
            }
        }

        [Authorize(Roles = "Admin")]
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
                {
                    return NotFound(new ResultViewModel<Product>("Product not found"));
                }
                else
                {
                    var category = await context
                        .Categories
                        .FirstOrDefaultAsync(x => x.Name == model.Category);

                    if (category == null)
                    {

                        product.Name = model.Name;
                        product.Price = model.Price;
                        product.Quantity = model.Quantity;
                        product.Category = new Category { Name = model.Category };
                    }
                    else
                    {
                        product.Name = model.Name;
                        product.Price = model.Price;
                        product.Quantity = model.Quantity;
                        product.Category = category;
                    }

                    context.Products.Update(product);
                    await context.SaveChangesAsync();

                    return Ok(new ResultViewModel<Product>(product));
                }
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Product>("05XE8 - Unable to change the product"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Product>("05X11 - Internal server failure"));
            }
        }


        [Authorize(Roles = "Admin")]
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
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Product>("05XE7 - Unable to delete product"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Product>("05X12 - Internal server failure"));
            }
        }


    }
}
