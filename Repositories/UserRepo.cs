using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Context;

namespace Project.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly MyDbContext context;

        public UserRepo(MyDbContext _context)
        {
            context = _context;
        }



        public UserProfile? getUser(string userName)
    {
            var IDParam = new SqlParameter("@Name", System.Data.SqlDbType.VarChar)
            {
                Value = userName
            };

            var returnedUser = context
                        .UserProfiles
                        .FromSqlRaw("exec userGetByName @Name",IDParam)
                        .ToList().FirstOrDefault();
            if (returnedUser!=null)
            {
                return returnedUser;
            }
            else
            {
                return null;
            }
    }

        bool IUserRepo.createUser(string userName, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            
            var NameParam = new SqlParameter("@Name", System.Data.SqlDbType.VarChar)
            {
                Value = userName
            };
            var PasswordParam = new SqlParameter("@PasswordHash", System.Data.SqlDbType.VarBinary)
            {
                Value = userPasswordHash
            };
            var SaltParam = new SqlParameter("@PasswordSalt", System.Data.SqlDbType.VarBinary)
            {
                Value = userPasswordSalt
            };
            try
            {
                context.Database.ExecuteSqlRaw("exec userCreate @Name, @PasswordHash, @PasswordSalt", NameParam, PasswordParam, SaltParam);
                return true;
            }
            catch (Exception)
            {
                return false;
                
            }
            
            
        }
    }
}
