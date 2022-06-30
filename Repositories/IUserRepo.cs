using Project.Context;

namespace Project.Repositories
{
    public interface IUserRepo
    {
        public UserProfile? userGetByName(string userName);
        public UserProfile? userGetByID(int ID);
        public List<UserProfile>? userGetAll();
        public void userDeleteByID(int ID);
        public void userDeleteByName(string name);
        public bool userCreate(string userName, byte[] userPasswordHash, byte[] userPasswordSalt);
    }
}
