using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace StudentManagement.Infrastructure.Data
{
    public class DesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<StudentManagementDbContext>
    {
        public StudentManagementDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString =
                configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<StudentManagementDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new StudentManagementDbContext(options);
        }
    }
}
