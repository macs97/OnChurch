
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnChurch.Common.Entities;
using OnChurch.Web.Data.Entities;

namespace OnChurch.Web.Data
{
    public class DataContext : IdentityDbContext<Member>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Campus> Campuses { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Church> Churches { get; set; }

        public DbSet<Profession> Professions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Campus>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<Section>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<Profession>()
                .HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}
