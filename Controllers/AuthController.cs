using Microsoft.AspNetCore.Mvc;
using Project.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly User user = new();

        [HttpPost]
        [Route("register")]
        public void Register([FromBody] DTOUser requesteduser)
        {

            user.userName = requesteduser.userName;
            HashPassword(requesteduser.password, out byte[] passwordhash, out byte[] passwordsalt);
            user.passwordSalt = passwordsalt;
            user.passwordHash = passwordhash;
        }

        private void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmacsha = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmacsha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordSalt = hmacsha.Key;
            }
        }
    }
}
