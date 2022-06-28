using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Project.Services
{
    public class AuthorizationService : IAuthorizationService
    {
    public int solveTokenUserID(HttpContext context)
        {
            context.Request.Headers.TryGetValue("Authorization", out var value);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(value);
            var tokenS = jsonToken as JwtSecurityToken;
            var userID = int.Parse(tokenS?.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            return userID;
        }
    }

}
