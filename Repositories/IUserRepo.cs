using Project.Context;

namespace Project.Repositories
{
    public interface IUserRepo
    {
        public UserProfile? getUser(string userName);
        public bool createUser(string userName, byte[] userPasswordHash, byte[] userPasswordSalt);
    }
}
