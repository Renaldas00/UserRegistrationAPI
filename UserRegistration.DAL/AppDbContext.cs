using Microsoft.EntityFrameworkCore;
using UserRegistration.DAL.Entities;

namespace UserRegistration.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PersonalDataList> PersonalDataLists { get; set; }
        public DbSet<LocationList> LocationList { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
