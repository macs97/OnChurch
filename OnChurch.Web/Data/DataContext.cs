
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnChurch.Web.Data.Entities;

namespace OnChurch.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Campus> Campuses { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Church> Churches { get; set; }

        public DbSet<Profession> Professions { get; set; }

        public DbSet<Meeting> Meetings { get; set; }

        public DbSet<Assistance> Assistances { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Campus>(campus =>
            {
                campus.HasIndex("Name").IsUnique();
                campus.HasMany(c => c.Sections).WithOne(s => s.Campus).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Section>(section =>
            {
                section.HasIndex("Name", "CampusId").IsUnique();
                section.HasOne(d => d.Campus).WithMany(c => c.Sections).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Church>(church =>
            {
                church.HasIndex("Name", "IdSection").IsUnique();
                church.HasOne(c => c.Section).WithMany(d => d.Churches).OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
