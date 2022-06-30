using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Context;
using Project.Filters;
using Project.Model;
using Project.Repositories;
using Project.Services;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[CustomAuthorization]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _categoryRepository;
        private readonly IAuthorizationService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryController(ICategoryRepo categoryrepository, IAuthorizationService authService, IHttpContextAccessor httpContextAccessor)
        {
            this._categoryRepository = categoryrepository;
            this._authService = authService;
            this._httpContextAccessor = httpContextAccessor;

        }

        [HttpPost("create")]
        public bool AddCategory([FromBody]DTOCategory requestedCategory)
        {
            var context = _httpContextAccessor.HttpContext;
            var createdBy = _authService.solveTokenUserID(context);
            return _categoryRepository.categoryCreate(requestedCategory.name, requestedCategory.description, createdBy);
        }
        [HttpDelete("byID")]
        public void categoryDeleteByID(int ID)
        {
            _categoryRepository.categorydeleteByID(ID);
        }

        [HttpDelete("byname")]
        public void categoryDeleteByName(string name)
        {
            _categoryRepository.categorydeleteByName(name);
        }
        
        [HttpGet("byID")]
        public CategoryProfile? categoryGetByID(int ID)
        {
            return _categoryRepository.categoryGetByID(ID);
        }

        [HttpGet("all")]
        public List<CategoryProfile>? categoryGetAll()
        {
            return _categoryRepository.categoryGetAll();
        }

        [HttpGet("byname")]
        public CategoryProfile? categoryGetByName(string name)
        {
            return _categoryRepository.categoryGetByName(name);
        }

    }
}
