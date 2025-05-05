using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using ShiftManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Design;

namespace ShiftManagementSystem.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Adjust the base path for .NET 9.0
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../ShiftManagementSystem.API"));
            Console.WriteLine("Base Path: " + basePath); // Log base path for debugging

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath) // Set base path correctly for .NET 9.0
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
