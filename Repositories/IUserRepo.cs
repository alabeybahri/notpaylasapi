using Project.Context;

namespace Project.Repositories
{
    public interface IUserRepo
    {
        public int getUser(int userID);
        public int createUser(string userName, string userPasswordHash);
    }
}
