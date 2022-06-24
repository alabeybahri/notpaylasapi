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
            this.context = _context;
        }



        public int getUser(int userID)
    {
            var IDParam = new SqlParameter("@ID", System.Data.SqlDbType.Int)
            {
                Value = userID
            };

            var returnedUser = context
                        .UserProfiles
                        .FromSqlRaw("exec getUser @ID",IDParam)
                        .ToList();
            
            return 0;
    }

        int IUserRepo.createUser(string userName, string userPasswordHash)
        {
            
            var NameParam = new SqlParameter("@Name", System.Data.SqlDbType.VarChar)
            {
                Value = userName
            };
            var PasswordParam = new SqlParameter("@PasswordHash", System.Data.SqlDbType.NVarChar)
            {
                Value = userPasswordHash
            };
            Console.WriteLine(userPasswordHash);
            context.Database.ExecuteSqlRaw("exec createUser @Name, @PasswordHash", NameParam, PasswordParam);
            return 0;
        }
    }
}
