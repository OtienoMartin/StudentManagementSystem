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

                // Build the service provider
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database context (StudentManagementDbContext)
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Ensure the database is created
                db.Database.EnsureCreated();

                // Optional: Clear all data before each test run to avoid conflicts
                // WARNING: Only do this if you want tests isolated from each other
                // Remove all entities from each DbSet manually or recreate the database
                db.Courses.RemoveRange(db.Courses);
                db.Students.RemoveRange(db.Students);
                db.Enrollments.RemoveRange(db.Enrollments);
                db.SaveChanges();
            });
        }
    }
}
