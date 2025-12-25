
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Infrastructure.Configuration;
using StudentManagement.Infrastructure.Data;

namespace StudentManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var dbOptions = new DatabaseOptions();
            configuration.GetSection("Database").Bind(dbOptions);

            services.AddDbContext<StudentManagementDbContext>(options =>
                options.UseSqlite(dbOptions.ConnectionString));

            return services;
        }
    }
}
