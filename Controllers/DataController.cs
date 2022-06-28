using Microsoft.AspNetCore.Mvc;
using Project.Context;
using Project.Filters;
using Project.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IUserRepo _userRepository;
        private readonly ICategoryRepo _categoryRepository;
        public DataController(IUserRepo userrepository, ICategoryRepo categoryrepository)
        {
            _userRepository = userrepository;
            _categoryRepository = categoryrepository;
        }

        // GET api/<DataController>/5
        [HttpGet]
        //[CustomAuthorization]
        public List<CategoryProfile> Get()
        {
            var categories = _categoryRepository.categoryGetAll();
            return categories;
        }

        // POST api/<DataController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


        // PUT api/<DataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
