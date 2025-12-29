using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Infrastructure.Data;
using System.Linq;

namespace StudentManagement.API.Tests.Infrastructure
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        // Static database name ensures the in-memory DB instance is shared across tests
        private static readonly string InMemoryDbName = "InMemoryStudentManagementTestDb";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // Remove existing DbContext registrations (like SQLite)
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<StudentManagementDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Register InMemory DB for tests with static database name to share data
                services.AddDbContext<StudentManagementDbContext>(options =>
                {
                    options.UseInMemoryDatabase(InMemoryDbName);
                });

                // Build the service provider and initialize the DB once (avoid recreating it for each test)
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Only ensure DB is created, but do NOT delete it to keep data between tests
                if (db.Database.IsInMemory())
                {
                    db.Database.EnsureCreated();
                }
            });
        }
    }
}
