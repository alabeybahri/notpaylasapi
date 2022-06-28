using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public ActionResult<string> Register([FromBody] DTOUser requesteduser)
        {
            var userName = requesteduser.userName;
            HashPassword(requesteduser.password, out byte[] passwordhash, out byte[] passwordsalt);

            if (userRepository.createUser(userName, passwordhash, passwordsalt))
            {
                return Ok();
            }
            return Unauthorized("\"User already exists \"");
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<string> Login(DTOUser requesteduser)
        {
            var userName = requesteduser.userName;
            var DBUser = userRepository.getUser(userName);

            if (DBUser == null)
            {
                return Unauthorized("Invalid password or username");
            }
            if (CheckPassword(requesteduser.password, DBUser.PasswordHash, DBUser.PasswordSalt))
            {
                var user = new User(DBUser.ID, userName, DBUser.PasswordHash, DBUser.PasswordSalt);
                var token = CreateToken(user);
                return Ok(token);
            }
            return Unauthorized("Invalid password or username");
        }


        [HttpPost]
        [Route("AddNote")]
        [CustomAuthorization]
        public bool AddNote(DTONote requestedNote)
        {
            var context = _httpContextAccessor.HttpContext;
            var createdBy = _authService.solveTokenUserID(context);
            int categoryID = int.Parse(requestedNote.Category);
            return noteRepository.noteCreate(requestedNote.Title, createdBy, categoryID, requestedNote.NoteValue);
        }

        [HttpPost]
        [Route("AddCategory")]
        [CustomAuthorization]
        public bool AddCategory(DTOCategory requestedCategory)
        {
            var context = _httpContextAccessor.HttpContext;
            var createdBy = _authService.solveTokenUserID(context);
            return categoryRepository.categoryCreate(requestedCategory.name, requestedCategory.description, createdBy);
        }

        //public bool AddCategory(DTONote requestedNote)
        //{
        //    var context = _httpContextAccessor.HttpContext;
        //    var createdBy = _authService.solveTokenUserID(context);
        //    int categoryID = int.Parse(requestedNote.Category);
        //    return noteRepository.noteCreate(requestedNote.Title, createdBy, categoryID, requestedNote.NoteValue);

        //}

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
