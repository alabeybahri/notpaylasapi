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
        public bool noteCreate(string Title,int CreatedBy, int CategoryID, string NoteValue,string? FileValue,string? FileType)
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
            var FileValueParam = new SqlParameter("@FileValue", System.Data.SqlDbType.NVarChar)
            {
                Value = FileValue
            };
            var FileTypeParam = new SqlParameter("@FileType", System.Data.SqlDbType.VarChar)
            {
                Value = FileType
            };

            try
            {
                context.Database.ExecuteSqlRaw("exec noteCreate @Title, @CreatedBy, @CategoryID, @NoteValue, @FileValue, @FileType", TitleParam, CreatedByParam, CategoryIDParam, NoteValueParam,FileValueParam,FileTypeParam);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void noteDeleteByID(int ID)
        {
            var IDParam = new SqlParameter("@ID", System.Data.SqlDbType.Int)
            {
                Value = ID
            };
            context.Database.ExecuteSqlRaw("exec noteDeleteByID @ID", IDParam);
        }

        public List<NoteProfile>? noteGetAll()
        {
            var noteProfiles = context.NoteProfiles.FromSqlRaw("exec noteGetAll").ToList();
            return noteProfiles;
        }

        public List<NoteProfile>? noteGetByCategoryID(int CategoryID)
        {
            var CategoryIDParam = new SqlParameter("@CategoryID", System.Data.SqlDbType.Int)
            {
                Value = CategoryID
            };
            var noteProfile = context.NoteProfiles.FromSqlRaw("exec noteGetByCategoryID @CategoryID", CategoryIDParam).ToList();
            return noteProfile;
        }

        public List<NoteProfile>? noteGetByCreatorID(int CreatorID)
        {
            var IDParam = new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int)
            {
                Value = CreatorID
            };
            var notes = context.NoteProfiles.FromSqlRaw("exec noteGetByCreatorID @CreatedBy", IDParam).ToList();
            return notes;
        }


        public NoteProfileWUserName? noteGetByID(int ID)
        {
            var IDParam = new SqlParameter("@ID", System.Data.SqlDbType.Int)
            {
                Value = ID
            };
            var note = context.NoteProfileWUserNames.FromSqlRaw("exec noteGetByID @ID", IDParam).ToList().FirstOrDefault();
            return note;

        }

        public List<NoteProfile>? noteGetByUpdaterID(int UpdaterID)
        {
            var UpdatedByParam = new SqlParameter("@UpdatedBy", System.Data.SqlDbType.Int)
            {
                Value = UpdaterID
            };
            var note = context.NoteProfiles.FromSqlRaw("exec noteGetByUpdaterID @UpdatedBy", UpdatedByParam).ToList();
            return note;
        }

        public void noteUpdate(int ID,string Title,string NoteValue,string UpdatedAt,int UpdatedBy)
        {
            throw new NotImplementedException();
        }

    }
}
