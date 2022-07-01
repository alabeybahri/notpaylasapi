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
        public RatingController(IRatingRepo ratingrepository, IAuthorizationService authService, IHttpContextAccessor httpContextAccessor)
        {
            _ratingRepository = ratingrepository;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("addrating")]
        public void AddRating([FromBody] DTORating requestedRating)
        {
            var context = _httpContextAccessor.HttpContext;
            var requestedBy = _authService.solveTokenUserID(context);
            var rating = new Rate(requestedRating.NoteID, requestedRating.Rating, requestedBy);
            _ratingRepository.ratingCreate(rating);
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



    }
}
