using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Context;
using Project.Filters;
using Project.Repositories;
using Project.Services;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorization]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepository;
        public UserController(IUserRepo userepository)
        {
            _userRepository = userepository;

        }

        [HttpDelete("byID")]
        public void userDeleteByID(int ID)
        {
            _userRepository.userDeleteByID(ID);
        }

        [HttpDelete("byname")]
        public void userDeleteByName(string name)
        {
            _userRepository.userDeleteByName(name);
        }

        [HttpGet("all")]
        public List<UserProfile>? userGetAll()
        {
            return _userRepository.userGetAll();
        }
        [HttpGet("byName")]
        public UserProfile? userGetByName(string name)
        {
            return _userRepository.userGetByName(name);
        }
        [HttpGet("byID")]
        public UserProfile? userGetByID(int ID)
        {
            return _userRepository.userGetByID(ID);
        }

    }
}
