using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Project.Filters;
using Project.Model;
using Project.Repositories;
using Project.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepo userRepository;
        private readonly INoteRepo noteRepository;
        private readonly ICategoryRepo categoryRepository;
        private readonly IAuthorizationService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthController(IUserRepo userRepository, INoteRepo noteRepository, ICategoryRepo categoryRepository, 
            IAuthorizationService authService, IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
            this.noteRepository = noteRepository;
            this.categoryRepository = categoryRepository;
            this._authService = authService;
            this._httpContextAccessor = httpContextAccessor;

        }
        private static byte[] keyBase = Encoding.Default.GetBytes("base key for bahri alabey");

        [HttpPost]
        [Route("Register")]
        public bool Register([FromBody] DTOUser requesteduser)
        {
            var userName = requesteduser.userName;
            HashPassword(requesteduser.password, out byte[] passwordhash, out byte[] passwordsalt);

            if (userRepository.userCreate(userName, passwordhash, passwordsalt))
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<string> Login(DTOUser requesteduser)
        {
            var userName = requesteduser.userName;
            var DBUser = userRepository.userGetByName(userName);

            if (DBUser == null)
            {
                return NoContent();
            }
            if (CheckPassword(requesteduser.password, DBUser.PasswordHash, DBUser.PasswordSalt))
            {
                var user = new User(DBUser.ID, userName, DBUser.PasswordHash, DBUser.PasswordSalt);
                var token = CreateToken(user);
                var loginObject = new
                {
                    token = token,
                    userName = userName,
                    userID = DBUser.ID,
                };
                string jsonData = JsonConvert.SerializeObject(loginObject);
                return Ok(jsonData);
            }
            return Unauthorized("Invalid password or username");
        }

        private void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmaic = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmaic.Key;
                passwordHash = hmaic.ComputeHash(Encoding.Default.GetBytes(password));
            }
        }
        private bool CheckPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmacsha = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmacsha.ComputeHash(Encoding.Default.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }


        private string CreateToken(User user)
        {
            List<Claim> claim = new()
            {
                new Claim(ClaimTypes.Name,user.userName),
                new Claim(ClaimTypes.NameIdentifier,user.ID.ToString())
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
