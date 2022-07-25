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
    [CustomAuthorization()]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepo _noteRepository;
        private readonly IAuthorizationService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NoteController(INoteRepo noteRepository,IAuthorizationService authService, IHttpContextAccessor httpContextAccessor)
        {
            this._noteRepository = noteRepository;
            this._authService = authService;
            this._httpContextAccessor = httpContextAccessor;

        }

        [HttpPost]
        [Route("addnote")]

        public bool AddNote([FromBody]DTONote requestedNote)
        {
            var context = _httpContextAccessor.HttpContext;
            var createdBy = _authService.solveTokenUserID(context);
            int categoryID = int.Parse(requestedNote.Category);
            return _noteRepository.noteCreate(requestedNote.Title, createdBy, categoryID, requestedNote.NoteValue,requestedNote.FileValue,requestedNote.FileType);
        }


        [HttpDelete("byID")]
        public void noteDeleteByID(int ID)
        {
            _noteRepository.noteDeleteByID(ID);
        }

        [HttpGet("all")]
        public List<NoteProfile>? GetNotes()
        {
            return _noteRepository.noteGetAll();
        }

        [HttpGet("bycategoryID")]
        public List<NoteProfile>? noteGetByCategoryID(int categoryID)
        {
            var note = _noteRepository.noteGetByCategoryID(categoryID);
            return note;
        }
        [HttpGet("bycreatorID")]
        public List<NoteProfile>? noteGetByCreatorID(int creatorID)
        {
            var note = _noteRepository.noteGetByCreatorID(creatorID);
            return note;
        }
        [HttpGet("byID")]
        public NoteProfileWUserName? noteGetByID(int ID)
        {
            var note = _noteRepository.noteGetByID(ID);
            return note;
        }
        [HttpGet("byupdaterID")]
        public List<NoteProfile>? noteGetByUpdaterID(int ID)
        {
            var note = _noteRepository.noteGetByUpdaterID(ID);
            return note;
        }
    }
}
