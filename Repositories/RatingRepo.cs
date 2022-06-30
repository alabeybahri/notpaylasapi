using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Model;

namespace Project.Repositories
{
    public class RatingRepo : IRatingRepo
    {
        private readonly MyDbContext _context;

        public RatingRepo(MyDbContext context)
        {
            _context = context;
        }

        public void ratingCreate(Rate requestedRating)
        {
            var NoteIDParam = new SqlParameter("@NoteID", System.Data.SqlDbType.Int)
            {
                Value = requestedRating.NoteID
            };
            var RatingParam = new SqlParameter("@Rating", System.Data.SqlDbType.Int)
            {
                Value = requestedRating.Rating
            };
            var UserIDParam = new SqlParameter("@UserID", System.Data.SqlDbType.Int)
            {
                Value = requestedRating.UserID
            };
            try
            {
                _context.Database.ExecuteSqlRaw("exec ratingCreate @NoteID, @Rating, @UserID", NoteIDParam, RatingParam, UserIDParam);
            }
            catch (Exception)
            { //INSTEAD OF TRY CATCH BLOCK ILL FIRST CONTROL IF RATING EXISTS OR NOT
                _context.Database.ExecuteSqlRaw("exec ratingUpdate @NoteID, @Rating, @UserID", NoteIDParam, RatingParam, UserIDParam);

            }
            
        }
    }
}
