using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Context;

namespace Project.Repositories
{

    public class NoteRepo : INoteRepo
    {
        private readonly MyDbContext context;

        public NoteRepo(MyDbContext _context)
        {
            context = _context;
        }
        public bool noteCreate(string Title,int CreatedBy, int CategoryID, string NoteValue)
        {
            var TitleParam = new SqlParameter("@Title", System.Data.SqlDbType.VarChar)
            {
                Value = Title
            };
            var CreatedByParam = new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int)
            {
                Value = CreatedBy
            };
            var CategoryIDParam = new SqlParameter("@CategoryID", System.Data.SqlDbType.Int)
            {
                Value = CategoryID
            };
            var NoteValueParam = new SqlParameter("@NoteValue", System.Data.SqlDbType.NVarChar)
            {
                Value = NoteValue
            };
            try
            {
                context.Database.ExecuteSqlRaw("exec noteCreate @Title, @CreatedBy, @CategoryID, @NoteValue", TitleParam, CreatedByParam, CategoryIDParam, NoteValueParam);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void noteDeleteByID(int ID)
        {
            throw new NotImplementedException();
        }

        public List<NoteProfile> noteGetAll()
        {
            var noteProfiles = context.NoteProfiles.FromSqlRaw("exec noteGetAll").ToList();
            return noteProfiles;
        }

        public List<NoteProfile> noteGetByCategoryID(int CategoryID)
        {
            throw new NotImplementedException();
        }

        public List<NoteProfile> noteGetByCreatorID(int CreatorID)
        {
            throw new NotImplementedException();
        }

        public NoteProfile noteGetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public List<NoteProfile> noteGetByUpdaterID(int UpdaterID)
        {
            throw new NotImplementedException();
        }

        public void noteUpdate(int ID,string Title,string NoteValue,string UpdatedAt,int UpdatedBy)
        {
            throw new NotImplementedException();
        }

    }
}
