namespace Project.Model
{
    public class DTONote
    {
        public DTONote(string title, string category, string noteValue)
        {
            Title = title;
            Category = category;
            NoteValue = noteValue;
        }
        public string Title { get; set; }   
        public string Category{ get; set; }
        public string NoteValue{ get; set; }

    }


}
