using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model;
using Project.Repositories;
using Project.Services;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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




    }
}
