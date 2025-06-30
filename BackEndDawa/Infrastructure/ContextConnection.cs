
using BackEndDawa.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndDawa.Infrastructure
{
    public class ContextConnection : DbContext
    {
        //DbSets here
        public DbSet<Company> Companies{ get; set; }

        public ContextConnection(DbContextOptions<ContextConnection> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primary keys below
            modelBuilder.Entity<Company>().HasKey(c => c.Id);

            //Foregein keys below

            //Others relations below
        }

    }
}
