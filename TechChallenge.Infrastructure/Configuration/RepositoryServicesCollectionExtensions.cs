using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TechChallenge.Data;
using TechChallenge.Domain.Leads;
using TechChallenge.Infrastructure.Repository;

namespace TechChallenge.Infrastructure.Configuration
{
    public static class RepositoryServicesCollectionExtensions
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
    }
}
