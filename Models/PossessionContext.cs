using Microsoft.EntityFrameworkCore;

namespace possession_backend.Models
{
    public class PossessionContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Container> Containers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var items = modelBuilder.Entity<Item>();
            items.Property(x => x.ItemId).IsRequired().ValueGeneratedOnAdd();
            items.HasKey(x => x.ItemId);
            items.HasOne(x => x.Category).WithMany(y => y.Items).HasForeignKey(z => z.CategoryId);
            items.HasOne(x => x.Location).WithMany(y => y.Items).HasForeignKey(z => z.LocationId).OnDelete(DeleteBehavior.Restrict);
            items.HasOne(x => x.Container).WithMany(y => y.Items).HasForeignKey(z => z.ContainerId);

            var categories = modelBuilder.Entity<Category>();
            categories.Property(x => x.CatrgoryId).IsRequired().ValueGeneratedOnAdd();
            categories.HasKey(c => c.CatrgoryId);

            var containers = modelBuilder.Entity<Container>();
            containers.Property(x => x.ContainerId).IsRequired().ValueGeneratedOnAdd();
            containers.HasKey(c => c.ContainerId);
            containers.HasOne(c => c.Location).WithMany(y => y.Containers).HasForeignKey(z => z.LocationId);

            var locations = modelBuilder.Entity<Location>();
            locations.Property(x => x.LocationId).IsRequired().ValueGeneratedOnAdd();
            locations.HasKey(l => l.LocationId);

            locations.HasMany(l => l.Items).WithOne(e => e.Location).HasForeignKey(e => e.LocationId);
            locations.HasMany(l => l.Containers).WithOne(c => c.Location).HasForeignKey(e => e.LocationId);

        }

        public PossessionContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
