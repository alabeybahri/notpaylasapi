using Project.Model;

namespace Project.Repositories
{
    public interface IRatingRepo
    {
        public void ratingCreate(Rate requestedRating);
    }
}
