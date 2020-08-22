
using Microsoft.EntityFrameworkCore;
using OnChurch.Common.Entities;

namespace OnChurch.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Campus> Campuses { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Church> Churches { get; set; }

        public DbSet<Member> Members { get; set; }

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
