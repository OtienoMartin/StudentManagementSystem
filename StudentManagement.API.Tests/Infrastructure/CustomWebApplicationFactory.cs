using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Infrastructure.Data;
using System.Linq;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // 🔴 REMOVE existing DbContext registrations
            var dbContextDescriptor = services
                .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<StudentManagementDbContext>));

            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            // ✅ ADD InMemory database for tests
            services.AddDbContext<StudentManagementDbContext>(options =>
            {
                options.UseInMemoryDatabase("StudentManagement_TestDb");
            });

            // Build service provider & ensure DB exists
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();
            db.Database.EnsureCreated();
        });
    }
}
