using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly User user = new();
        private static byte[] keyBase = System.Text.Encoding.UTF8.GetBytes("base key for bahri alabey");

        [HttpPost]
        [Route("Register")]
        public void Register([FromBody] DTOUser requesteduser)
        {
            user.userName = requesteduser.userName;
            HashPassword(requesteduser.password, out byte[] passwordhash, out byte[] passwordsalt);
            user.passwordSalt = passwordsalt;
            user.passwordHash = passwordhash;
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<string> Login(DTOUser requesteduser)
        {
            if (CheckPassword(requesteduser.password, user.passwordHash, user.passwordSalt) && requesteduser.userName.Equals(user.userName))
            {
                var token = CreateToken(user);
                return Ok(token);
            }
            return Unauthorized("Invalid password or username");
        }
        private void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmacsha = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmacsha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordSalt = hmacsha.Key;
            }
        }

        private bool CheckPassword(string password, byte[] realPasswordHash, byte[] realPasswordSalt)
        {
            using(var hmacsha = new System.Security.Cryptography.HMACSHA512(realPasswordSalt))
            {
                var tryPasswordHash = hmacsha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return tryPasswordHash.SequenceEqual(realPasswordHash);
            }   
        }

        private string CreateToken(User user)
        {
            List<Claim> claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.userName)
            };

            var key = new SymmetricSecurityKey(keyBase);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claim,
                signingCredentials: creds,
                expires: DateTime.Now.AddDays(1)
                );

            var jwtoken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtoken;
        }
    }
}
