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



        public UserProfile? userGetByName(string userName)
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

        public bool userCreate(string userName, byte[] userPasswordHash, byte[] userPasswordSalt)
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

        public UserProfile? userGetByID(int ID)
        {
            var IDParam = new SqlParameter("@ID", System.Data.SqlDbType.Int)
            {
                Value = ID
            };
            return context.UserProfiles.FromSqlRaw("exec userGetByID @ID",IDParam).ToList().FirstOrDefault();
        }

        public List<UserProfile>? userGetAll()
        {
            return context.UserProfiles.FromSqlRaw("exec userGetAll").ToList();
        }

        public void userDeleteByID(int ID)
        {
            var IDParam = new SqlParameter("@ID", System.Data.SqlDbType.Int)
            {
                Value = ID
            };
            context.Database.ExecuteSqlRaw("exec userDeleteByID @ID", IDParam);
        }

        public void userDeleteByName(string name)
        {
            var NameParam = new SqlParameter("@Name", System.Data.SqlDbType.VarChar)
            {
                Value = name
            };
            context.Database.ExecuteSqlRaw("exec userDeleteByName @Name", NameParam);
        }
    }
}
