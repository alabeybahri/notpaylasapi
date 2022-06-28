namespace Project.Model
{
    public class User
    {
        public User(int ID,string userName, byte[] passwordHash, byte[] passwordSalt)
        {
            this.ID = ID;
            this.userName = userName;
            this.passwordHash = passwordHash;
            this.passwordSalt = passwordSalt;
        }

        public int ID { get; set; }
        public string userName { get; set; } = String.Empty;
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
    }

    
}
