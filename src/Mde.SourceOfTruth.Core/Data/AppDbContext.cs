using Mde.SourceOfTruth.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Mde.SourceOfTruth.Core.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    
        public DbSet<RegisteredDevice> RegisteredDevices { get; set; }
        public DbSet<Memoria> Memorias { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<MediaItem> MediaItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RegisteredDevice>()
                .HasMany(d => d.memorias)
                .WithOne(m => m.Device)
                .HasForeignKey(m => m.RegisteredDeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Memoria>()
               .HasOne(m => m.MemoriaAddress)
               .WithOne(a => a.Memoria)
               .HasForeignKey<Address>(a => a.MemoriaId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Memoria>()
                .HasMany(m => m.MediaMaterial)
                .WithOne(mi => mi.Memoria)
                .HasForeignKey(mi => mi.MemoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Memoria>()
                .Property(m => m.Occation)
                .HasConversion<string>();

            builder.Entity<MediaItem>()
                .Property(m => m.Type)
                .HasConversion<string>();

            base.OnModelCreating(builder);
            Seeding.Seeder.Seed(builder);
        }
    }
}
