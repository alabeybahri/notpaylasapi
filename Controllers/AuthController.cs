using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project.Filters;
using Project.Model;
using Project.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepo _repository;
        public AuthController(IUserRepo repository)
        {
            _repository = repository;
        }
        private static byte[] keyBase = System.Text.Encoding.UTF8.GetBytes("base key for bahri alabey");

        [HttpPost]
        [Route("Register")]
        public int Register([FromBody] DTOUser requesteduser)
        {
            var userName = requesteduser.userName;
            HashPassword(requesteduser.password, out byte[] passwordhash, out byte[] passwordsalt);
            var passwordHash = passwordhash;
            string bitString = BitConverter.ToString(passwordHash);
            return _repository.createUser(userName, bitString);
        }

        //[HttpPost]
        //[Route("Login")]
        //public ActionResult<string> Login(DTOUser requesteduser)
        //{

        //    if (CheckPassword(requesteduser.password, user.passwordHash, user.passwordSalt) && requesteduser.userName.Equals(user.userName))
        //    {
        //        var token = CreateToken(user);
        //        return Ok(token);
        //    }
        //    return Unauthorized("Invalid password or username");
        //}

        [HttpGet]
        [Route("deneme")]
        [CustomAuthorization]
        public string Deneme()
        {
            return "deneme text";
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
