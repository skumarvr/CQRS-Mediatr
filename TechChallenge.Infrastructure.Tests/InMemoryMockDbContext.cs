using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Data;
using TechChallenge.Data.EntityModels;

namespace TechChallenge.Infrastructure.Tests
{
    public static class InMemoryMockDbContext
    {
        public static hipagesDbContext databaseContext = null;

        public static async Task<hipagesDbContext> GetDatabaseContext()
        {
            if( databaseContext == null)
            {
                var options = new DbContextOptionsBuilder<hipagesDbContext>()
                                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                .Options;
                databaseContext = new hipagesDbContext(options);
                databaseContext.Database.EnsureCreated();

                await LoadCategories(databaseContext);
                await LoadSuburbs(databaseContext);
                await LoadJobs(databaseContext);
            }

            return databaseContext;
        }

        private static async Task LoadJobs(hipagesDbContext databaseContext)
        {
            if (await databaseContext.Jobs.CountAsync() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    databaseContext.Jobs.Add(new Jobs()
                    {
                        Id = i,
                        Status = "New",
                        SuburbId = i,
                        CategoryId = i,
                        ContactName = $"First{i} Last{i}",
                        ContactPhone = $"045012300{i}",
                        ContactEmail = $"contact{i}@expample.com",
                        Price = i*101,
                        Description = $"Description {i}",
                        CreatedAt = DateTime.Now
                    });
                }

                for (int i = 6; i <= 8; i++)
                {
                    databaseContext.Jobs.Add(new Jobs()
                    {
                        Id = i,
                        Status = "Accepted",
                        SuburbId = i%5,
                        CategoryId = i%5,
                        ContactName = $"First{i} Last{i}",
                        ContactPhone = $"045012300{i}",
                        ContactEmail = $"contact{i}@expample.com",
                        Price = i * 101,
                        Description = $"Description {i}",
                        CreatedAt = DateTime.Now
                    });
                }

                await databaseContext.SaveChangesAsync();
            }
        }

        private static async Task LoadSuburbs(hipagesDbContext databaseContext)
        {
            if (await databaseContext.Suburbs.CountAsync() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    databaseContext.Suburbs.Add(new Suburbs()
                    {
                        Id = i,
                        Name = $"suburb_{i}",
                        Postcode = $"200{i}"
                    });
                }
                await databaseContext.SaveChangesAsync();
            }
        }

        private static async Task LoadCategories(hipagesDbContext databaseContext)
        {
            if (await databaseContext.Categories.CountAsync() <= 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    databaseContext.Categories.Add(new Categories()
                    {
                        Id = i,
                        Name = $"category_{i}",
                        ParentCategoryId = i * 10
                    });
                }
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
