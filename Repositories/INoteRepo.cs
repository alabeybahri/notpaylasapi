using Project.Context;

namespace Project.Repositories
{
    public interface INoteRepo
    {
        public bool noteCreate(string Title,int CreatedBy,int CategoryID,string NoteValue);
        public void noteDeleteByID(int ID);
        public List<NoteProfile>? noteGetAll();
        public List<NoteProfile>? noteGetByCategoryID(int CategoryID);
        public List<NoteProfile>? noteGetByCreatorID(int CreatorID);
        public NoteProfile? noteGetByID(int ID);
        public List<NoteProfile>? noteGetByUpdaterID(int UpdaterID);
        public void noteUpdate(int ID, string Title, string NoteValue, string UpdatedAt, int UpdatedBy);


    }
}
