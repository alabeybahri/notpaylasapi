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
            //try
            //{
            //    var returnedCategory = context.CategoryProfiles.FromSqlRaw("exec categoryGetByName @Name",NameParam).ToList().FirstOrDefault();
            //    return returnedCategory;
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
        }

        public List<CategoryProfile> categoryGetAll()
        {
            var categoryProfiles = context.CategoryProfiles.FromSqlRaw("exec categoryGetAll").ToList();
            return categoryProfiles;
        }

        bool ICategoryRepo.categoryCreate(string name, string description, int userID)
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
            context.Database.ExecuteSqlRaw("exec categoryCreate @Name, @Description, @UserID", NameParam, DescParam, UserIDParam);
            return true;
        }
    }
}
