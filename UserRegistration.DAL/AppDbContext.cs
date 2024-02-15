using Microsoft.EntityFrameworkCore;
using UserRegistration.DAL.Entities;

namespace UserRegistration.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        // DbSet properties representing database tables for different entity types.

        public DbSet<Account> Account { get; set; }
        public DbSet<UserData> UserData { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Image> Image { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
            .HasOne(a => a.UserData)
            .WithOne(ud => ud.Account)
            .HasForeignKey<UserData>(ud => ud.AccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Image>()
                .HasOne(ud => ud.UserDataItem)
                .WithOne(p => p.Image)
                .HasForeignKey<Image>(p => p.UserDataItemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Location>()
                .HasOne(ud => ud.UserLocation)
                .WithOne(l => l.Location)
                .HasForeignKey<Location>(l => l.UserLocationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
