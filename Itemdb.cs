using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace Projekt
{

    public class Itemdb : DbContext
    {
        public Itemdb(DbContextOptions<Itemdb> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<Item> Itemos { get; set; }

    }
}
