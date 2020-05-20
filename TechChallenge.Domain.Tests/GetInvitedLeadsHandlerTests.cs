using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Handlers;
using TechChallenge.Domain.Leads.Models;
using TechChallenge.Domain.Leads.Queries;
using TechChallenge.Infrastructure.Repository;
using TechChallenge.Infrastructure.Tests;

namespace TechChallenge.Domain.Tests
{
    public class GetInvitedLeadsHandlerTests
    {
        public ILogger<T> CreateLogger<T>()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            return loggerFactory.CreateLogger<T>();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Should_Return_All_New_Leads()
        {
            var dbContext = await InMemoryMockDbContext.GetDatabaseContext();

            var leadHandlerRepository = new LeadHandlerRepository(dbContext);

            var leadHandler = new GetInvitedLeadsHandler(leadHandlerRepository, CreateLogger<GetInvitedLeadsHandler>());
            var query = new GetInvitedLeadsQuery();
            var resp = await leadHandler.Handle(query, new CancellationToken());

            var leadInDb = dbContext.Jobs.ToList().Where(x => x.Status == LeadStatusType.New.ToString()).ToList();

            Assert.NotNull(resp);
            Assert.AreEqual(resp.Count, leadInDb.Count);
        }

    }
}
