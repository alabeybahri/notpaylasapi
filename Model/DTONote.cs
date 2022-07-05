namespace Project.Model
{
    public class DTONote
    {
        public DTONote(string title, string category, string noteValue,string? fileValue,string? fileType)
        {
            Title = title;
            Category = category;
            NoteValue = noteValue;
            FileValue = fileValue;
            FileType = fileType;
        }
        public string Title { get; set; }   
        public string Category{ get; set; }
        public string NoteValue{ get; set; }
        public string? FileValue { get; set; }
        public string? FileType { get; set; }

    }


}
