using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Context
{
    public class MyDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public MyDbContext(DbContextOptions<MyDbContext> options, IConfiguration configuration)

        : base(options) 
        {
            Configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=NotPaylasDB");
        //}
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<NoteProfile> NoteProfiles { get; set; }
        public DbSet<CategoryProfile> CategoryProfiles { get; set; }
        //public DbSet<RatingProfile> RatingProfiles { get; set; }

    }


    public class UserProfile
    {
        [Key] 
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
    
    public class NoteProfile
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Users")]
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("Users")]
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("Categories")]
        public int CategoryID{ get; set; }
        public string NoteValue { get; set; }

    }

    public class CategoryProfile
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
    }

    //public class RatingProfile
    //{
    //    public int NoteID { get; set; }
    //    public int Rating { get; set; }
    //    public int UserID { get; set; }

    //}

}
