using ApiInventoryControl.Models;
using System.Security.Claims;

namespace ApiInventoryControl.Extensions
{
    public static class RoleClaimExtensions
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var result = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email)
        };
            result.AddRange(
                user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name))
            );
            return result;
        }
    }
}
