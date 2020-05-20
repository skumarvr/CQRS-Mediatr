using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Data;
using TechChallenge.Domain.Leads;
using TechChallenge.Domain.ServiceHandlers;
using TechChallenge.Infrastructure.Repository;
using TechChallenge.Infrastructure.ServiceHandlers;

namespace TechChallenge.Infrastructure.Configuration
{
    public static class InfrastructureServicesCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            // Adding DbContext
            services.AddDbContext<hipagesDbContext>(ServiceLifetime.Scoped);

            // Adding Repositories
            // services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<,>));
            services.AddScoped(typeof(ILeadHandlerRepository), typeof(LeadHandlerRepository));
            return services;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEmailServiceHandler), typeof(EmailServiceHandler));
            return services;
        }
    }
}
