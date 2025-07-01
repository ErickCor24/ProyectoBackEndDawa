
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
            modelBuilder.Entity<Client>().HasKey(c => c.Id);
            modelBuilder.Entity<Reserve>().HasKey(r => r.Id);
            modelBuilder.Entity<Vehicle>().HasKey(v => v.Id);
            modelBuilder.Entity<UserCompany>().HasKey(uc => uc.Id);
            modelBuilder.Entity<UserClient>().HasKey(uc => uc.Id);

            //Foregein keys below
            modelBuilder.Entity<Reserve>()
                .HasOne(r => r.Vehicle)
                .WithMany()
                .HasForeignKey(r => r.VehicleId);

            modelBuilder.Entity<Reserve>()
                .HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey(r => r.ClientId);


            modelBuilder.Entity<UserCompany>()
                .HasOne(uc => uc.Company)
                .WithMany()
                .HasForeignKey(uc => uc.CompanyId);

            modelBuilder.Entity<UserClient>()
                .HasOne(uc => uc.Client)
                .WithMany()
                .HasForeignKey(uc => uc.ClientId);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Company)
                .WithMany()
                .HasForeignKey(v => v.CompanyId);

            //Others relations below
        }

    }
}
