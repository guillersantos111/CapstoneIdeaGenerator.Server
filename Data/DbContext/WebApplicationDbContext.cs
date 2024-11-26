using Microsoft.EntityFrameworkCore;
using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
using CapstoneIdeaGenerator.Server.Entities.Models;

namespace CapstoneIdeaGenerator.Server.Data.DbContext
{
    public class WebApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Capstones> Capstones { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Ratings> Ratings { get; set; }

        public WebApplicationDbContext(DbContextOptions<WebApplicationDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Admins>()
                .HasKey(a => a.AdminId);

            builder.Entity<Capstones>()
                .HasKey(c => c.CapstoneId);

            builder.Entity<Capstones>()
                .HasMany(c => c.Ratings)
                .WithOne()
                .IsRequired(false);

            builder.Entity<Ratings>()
                .HasKey(r => r.RatingId);

            builder.Entity<Ratings>()
                .HasOne(r => r.Capstones)
                .WithMany(c => c.Ratings)
                .HasForeignKey(r => r.CapstoneId);
        }
    }
}
