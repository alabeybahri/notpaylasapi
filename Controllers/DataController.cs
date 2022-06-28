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
        private readonly INoteRepo _noteRepository;
        public DataController(IUserRepo userrepository, ICategoryRepo categoryrepository, INoteRepo noterepository)
        {
            _userRepository = userrepository;
            _categoryRepository = categoryrepository;
            _noteRepository = noterepository;
        }

        // GET api/<DataController>/5
        [HttpGet("categories")]
        //[CustomAuthorization]
        public List<CategoryProfile> GetCategories()
        {
            var categories = _categoryRepository.categoryGetAll();
            return categories;
        }

        [HttpGet("notes")]
        //[CustomAuthorization]
        public List<NoteProfile> GetNotes()
        {
            var notes = _noteRepository.noteGetAll();
            return notes;
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
