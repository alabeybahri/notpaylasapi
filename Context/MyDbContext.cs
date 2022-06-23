using Microsoft.EntityFrameworkCore;

namespace Project.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=NotPaylasDB");
        }
    }

}
