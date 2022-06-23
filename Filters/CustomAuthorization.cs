using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Project.Filters
{
    public class CustomAuthorization : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var value))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(value);
                var tokenS = jsonToken as JwtSecurityToken;
                var username = tokenS?.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            }
            else
            {
                context.Result = new ContentResult()
                {
                    StatusCode = (int)System.Net.HttpStatusCode.BadRequest,
                    Content = "Unauthorized"
                };
            }
        }
    }

}

