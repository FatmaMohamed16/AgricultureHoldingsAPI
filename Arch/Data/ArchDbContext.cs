using AmlakState.Models;
using Arch.Models;
using Microsoft.EntityFrameworkCore;

namespace AmlakState.Data
{
    public class ArchDbContext : DbContext
    {
        public ArchDbContext(DbContextOptions<ArchDbContext> options) : base(options)
        {
        }

        public DbSet<Markaz> Markazes { get; set; }
        public DbSet<AgriculturalHolding> AgriculturalHoldings { get; set; }
        public DbSet<PropertyCoordinate> PropertyCoordinates { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Madina_Maglas> Madina_Maglas { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<SourceOfOwnership> SourceOfOwnership { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<AgriculturalHolding>()
                .HasOne(a => a.User)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
               .HasOne(u => u.Markaz)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}