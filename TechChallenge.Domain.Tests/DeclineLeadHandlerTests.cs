using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Commands;
using TechChallenge.Domain.Leads.Handlers;
using TechChallenge.Domain.Leads.Models;
using TechChallenge.Infrastructure.Repository;
using TechChallenge.Infrastructure.Tests;

namespace TechChallenge.Domain.Tests
{
    public class DeclineLeadHandlerTests
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
        public async Task Should_Decline_A_New_Lead()
        {
            var dbContext = await InMemoryMockDbContext.GetDatabaseContext();

            var leadHandlerRepository = new LeadHandlerRepository(dbContext);

            var leads = await leadHandlerRepository.GetLeadsWithNewStatus();
            var job = leads.First();

            var leadHandler = new DeclineLeadHandler(leadHandlerRepository, CreateLogger<DeclineLeadHandler>());

            var cmd = new DeclineLeadCommand(job.Id);
            var resp = await leadHandler.Handle(cmd, new CancellationToken());

            var leadInDb = await leadHandlerRepository.GetLeadById(job.Id);

            Assert.NotNull(resp);
            Assert.AreEqual(job.Id, resp.JobId);
            Assert.AreEqual(LeadStatusType.Declined.ToString(), resp.Status);
            Assert.AreEqual(leadInDb.Price, job.Price);
            Assert.AreEqual(LeadStatusType.Declined.ToString(), leadInDb.Status);
        }
    }
}
