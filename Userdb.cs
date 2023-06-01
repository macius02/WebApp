using Microsoft.EntityFrameworkCore;

namespace Projekt
{
    public class Userdb : DbContext
    {
        public Userdb(DbContextOptions<Userdb> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<User> Users { get; set; }
    }
}
