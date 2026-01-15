using Microsoft.EntityFrameworkCore;
using LectorASPNET.Models;

namespace LectorASPNET.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users
        {
            get; set;
        }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserLibrary> UserLibraries { get; set; }
        public DbSet<Social> Socials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Social>()
                .HasKey(s => new { s.FollowerId, s.FollowingId }); 

            modelBuilder.Entity<Social>()
                .HasOne(s => s.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(s => s.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Social>()
                .HasOne(s => s.Following)
                .WithMany(u => u.Followers)
                .HasForeignKey(s => s.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .Property(r => r.Rating)
                .HasColumnType("double precision"); 
        }
    }
}