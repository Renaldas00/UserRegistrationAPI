using Microsoft.EntityFrameworkCore;
using UserRegistration.DAL.Entities;

namespace UserRegistration.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<UserDataList> UserDataList { get; set; }
        public DbSet<LocationList> LocationList { get; set; }
        public DbSet<Photo> Photo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
            .HasOne(a => a.UserDataList)
            .WithOne(ud => ud.Account)
            .HasForeignKey<UserDataList>(ud => ud.AccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserDataList>()
                .HasOne(ud => ud.Photo)
                .WithOne(p => p.UserPhoto)
                .HasForeignKey<Photo>(p => p.UserPhotoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserDataList>()
                .HasOne(ud => ud.Location)
                .WithOne(l => l.UserLocation)
                .HasForeignKey<LocationList>(l => l.UserLocationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
