using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Context;

namespace Project.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly MyDbContext context;

        public CategoryRepo(MyDbContext _context)
        {
            context = _context;
        }
        public CategoryProfile? categoryGetByName(string Name)
        {
            var NameParam = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar)
            {
                Value = Name
            };
            var returnedCategory = context.CategoryProfiles.FromSqlRaw("exec categoryGetByName @Name", NameParam).ToList().FirstOrDefault();
            return returnedCategory;
        }

        public List<CategoryProfile> categoryGetAll()
        {
            var categoryProfiles = context.CategoryProfiles.FromSqlRaw("exec categoryGetAll").ToList();
            return categoryProfiles;
        }

        public bool categoryCreate(string name, string description, int userID)
        {
            var NameParam = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar)
            {
                Value = name
            };
            var DescParam = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar)
            {
                Value = description
            };
            var UserIDParam = new SqlParameter("@UserID", System.Data.SqlDbType.Int)
            {
                Value = userID
            };
            try
            {
                context.Database.ExecuteSqlRaw("exec categoryCreate @Name, @Description, @UserID", NameParam, DescParam, UserIDParam);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public CategoryProfile? categoryGetByID(int ID)
        {
            var IDParam = new SqlParameter("@ID", System.Data.SqlDbType.Int)
            {
                Value = ID
            };
            var returnedCategory = context.CategoryProfiles.FromSqlRaw("exec categoryGetByID @ID", IDParam).ToList().FirstOrDefault();
            return returnedCategory;
        }

        public void categorydeleteByID(int ID)
        {
            var IDParam = new SqlParameter("@ID", System.Data.SqlDbType.Int)
            { 
                Value = ID
            };
            context.Database.ExecuteSqlRaw("exec categoryDeleteByID @ID", IDParam);
        }

        public bool categorydeleteByName(string name)
        {
            var NameParam = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar)
            {
                Value = name
            };
            try
            {
                context.Database.ExecuteSqlRaw("exec categoryDeleteByName @Name", NameParam);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
