using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Filters;
using Project.Model;
using Project.Repositories;
using Project.Services;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorization]
    public class RatingController : Controller
    {
        private readonly IRatingRepo _ratingRepository;
        private readonly IAuthorizationService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INoteRepo _noteRepository;
        
        public RatingController(IRatingRepo ratingrepository, IAuthorizationService authService, IHttpContextAccessor httpContextAccessor,INoteRepo noterepository)
        {
            _ratingRepository = ratingrepository;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _noteRepository = noterepository;
        }

        [HttpPost]
        [Route("addrating")]
        public bool AddRating([FromBody] DTORating requestedRating)
        {
            var context = _httpContextAccessor.HttpContext;
            var requestedBy = _authService.solveTokenUserID(context);
            var noteCreatorID = _noteRepository.noteGetByID(requestedRating.NoteID)?.CreatedBy;
            if (!noteCreatorID.Equals(requestedBy))
            {
            var rating = new Rate(requestedRating.NoteID, requestedRating.Rating, requestedBy);
            _ratingRepository.ratingCreate(rating);
            return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost("getrating")]
        public int? GetRating([FromBody] int NoteID)
        {
            var context = _httpContextAccessor.HttpContext;
            var userID = _authService.solveTokenUserID(context);
            var ratingValue = _ratingRepository.ratingGet(NoteID, userID);
            if (ratingValue == null)
            {
                return 0;
            }
            return ratingValue;
        }
        [HttpPost("getratingaverage")]
        public float? GetRatingAverage([FromBody] int NoteID)
        {
            var ratingValue = _ratingRepository.ratingGetAverage(NoteID);
            if (ratingValue == null)
            {
                return 0;
            }
            return ratingValue;
        }

        [HttpDelete("delete")]
        public bool DeleteRating(int NoteID, int UserID)
        {
            return _ratingRepository.ratingDelete(NoteID,UserID);
        }

    }
}
