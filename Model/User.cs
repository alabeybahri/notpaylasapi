namespace Project.Model
{
    public class User
    {
        public User(string userName, byte[] passwordHash, byte[] passwordSalt)
        {
            this.userName = userName;
            this.passwordHash = passwordHash;
            this.passwordSalt = passwordSalt;
        }

        public string userName { get; set; } = String.Empty;
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
    }

    
}
