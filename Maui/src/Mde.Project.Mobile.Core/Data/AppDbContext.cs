using Mde.Project.Mobile.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Emit;
using System.Text;

namespace Mde.Project.Mobile.Core.Data
{
    public class AppDbContext : DbContext
    {
        // DBSets
        public DbSet<Memoria> Memorias { get; set; }
        public DbSet<MediaItem> MediaItems { get; set; }
        public DbSet<Address> Addresses { get; set; }

        // constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // methoden
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Memoria>()
               .HasOne(m => m.MemoriaAddress)
               .WithOne(a => a.Memoria)
               .HasForeignKey<Memoria>(m => m.AddressId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Memoria>()
                .HasMany(m => m.MediaMaterial)
                .WithOne(mi => mi.Memoria)
                .HasForeignKey(mi => mi.MemoriaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
