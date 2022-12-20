using ApiInventoryControl.Data;
using ApiInventoryControl.Extensions;
using ApiInventoryControl.Models;
using ApiInventoryControl.Services;
using ApiInventoryControl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace ApiInventoryControl.Controllers
{
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {

        [AllowAnonymous]
        [HttpPost("v1/login")]
        public IActionResult Login(
            [FromServices] TokenService tokenService
            )
        {
            var token = tokenService.GenerateToken(null);

            return Ok(token);
        }


        [HttpPost("v1/accounts/")]
        public async Task<IActionResult> Post(
        [FromBody] RegisterViewModel model,
        [FromServices] InventoryDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
            };

            var password = PasswordGenerator.Generate(25);
            user.PasswordHash = PasswordHasher.Hash(password);

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    user = user.Email,
                    password
                }));
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new ResultViewModel<string>("04X99 - This email is already registered"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("95X04 - Internal server failure"));
            }
        }

        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login(
        [FromBody] LoginViewModel model,
        [FromServices] InventoryDataContext context,
        [FromServices] TokenService tokenService)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = await context
                .Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return StatusCode(401, new ResultViewModel<string>("username or password is invalid"));

            if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
                return StatusCode(401, new ResultViewModel<string>("username or password is invalid"));

            try
            {
                var token = tokenService.GenerateToken(user);
                return Ok(new ResultViewModel<string>(token, null));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("02P04 - Internal server failure"));
            }
        }

    }
}
