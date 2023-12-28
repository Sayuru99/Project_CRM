using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.SharedDomain.Repositories;
using User.Core.Models.User;
using User.Core.Models.User.Image;

namespace MyApp.Core.Users.Data.MySql.Contexts
{
    public class UserContext : EFContext
    {
        public UserContext() : base() { }
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ImageModel> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
           .HasOne(e => e.Image)
           .WithOne(e => e.UserMaster)
           .HasForeignKey<ImageModel>(e => e.Id)
           .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
