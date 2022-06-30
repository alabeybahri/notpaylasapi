namespace Project.Model
{
    public class Rate
    {
        public Rate(int noteID, int rating, int userID)
        {
            NoteID = noteID;
            Rating = rating;
            UserID = userID;
        }

        public int NoteID { get; set; }
        public int Rating { get; set; }
        public int UserID { get; set; }
    }
}
