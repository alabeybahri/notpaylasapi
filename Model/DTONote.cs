namespace Project.Model
{
    public class DTONote
    {
        public DTONote(string title, int createdBy, int updatedBy, bool ısDeleted, bool ısActive, int categoryID, string noteValue)
        {
            Title = title;
            CreatedBy = createdBy;
            UpdatedBy = updatedBy;
            IsDeleted = ısDeleted;
            IsActive = ısActive;
            CategoryID = categoryID;
            NoteValue = noteValue;
        }
        public DTONote(string title, int createdBy, int categoryID, string noteValue)
        {
            Title = title;
            CreatedBy = createdBy;
            CategoryID = categoryID;
            NoteValue = noteValue;
        }
        public string Title { get; set; }   
        public int CreatedBy { get; set; }
        public int UpdatedBy{ get; set; }
        public bool IsDeleted{ get; set; }
        public bool IsActive{ get; set; }
        public int CategoryID{ get; set; }
        public string NoteValue{ get; set; }

    }
}
