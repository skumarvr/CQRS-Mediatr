using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TechChallenge.Data.Configuration
{
    public static class DatabaseServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<hipagesDbContext>(o => o.UseMySQL(dbConnectionString));
            return services;
        }
    }
}