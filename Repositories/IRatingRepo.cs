using Project.Context;
using Project.Model;

namespace Project.Repositories
{
    public interface IRatingRepo
    {
        public void ratingCreate(Rate requestedRating);
        public int? ratingGet(int NoteID, int UserID);
        public float? ratingGetAverage(int NoteID);
    }
}
