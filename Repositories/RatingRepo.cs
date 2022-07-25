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
            { 
                _context.Database.ExecuteSqlRaw("exec ratingUpdate @NoteID, @Rating, @UserID", NoteIDParam, RatingParam, UserIDParam);
            }
            
        }

        public int? ratingGet(int NoteID, int UserID)
        {
            
            var NoteIDParam = new SqlParameter("@NoteID", System.Data.SqlDbType.Int)
            {
                Value = NoteID
            };
            var UserIDParam = new SqlParameter("@UserID", System.Data.SqlDbType.Int)
            {
                Value = UserID
            };
            var rating = _context.RatingProfiles.FromSqlRaw("exec ratingGet @NoteID, @UserID", NoteIDParam, UserIDParam).ToList().FirstOrDefault()?.Rating;
            return rating;

        }

        public float? ratingGetAverage(int NoteID)
        {

            var NoteIDParam = new SqlParameter("@NoteID", System.Data.SqlDbType.Int)
            {
                Value = NoteID
            };
            var ratings = _context.RatingProfiles.FromSqlRaw("exec ratingGetAverage @NoteID", NoteIDParam).ToList();
            var sum = 0;
            var total= 0;
            foreach (var rating in ratings)
            {
                sum += rating.Rating;
                total ++;
            }
            if(total == 0)
            {
                return 0;
            }
            return (float)sum/total;

        }

        public bool ratingDelete(int NoteID,int UserID)
        {
            var NoteIDParam = new SqlParameter("@NoteID", System.Data.SqlDbType.Int)
            {
                Value = NoteID
            };
            var UserIDParam = new SqlParameter("@UserID", System.Data.SqlDbType.Int)
            {
                Value = UserID
            };
            try
            {
                _context.Database.ExecuteSqlRaw("exec ratingDelete @NoteID, @UserID", NoteIDParam, UserIDParam);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
