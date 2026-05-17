using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Voor migrations gebruik een vaste locatie
            var path = Path.Combine(Directory.GetCurrentDirectory(), "app.db");
            optionsBuilder.UseSqlite($"Filename={path}");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
