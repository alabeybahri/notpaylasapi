namespace Project.Model
{
    public class User
    {
        public string userName { get; set; } = String.Empty;
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
    }
}
