using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
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
    public class AcceptLeadHandlerTests
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
        public async Task Should_Accept_A_New_Lead_With_Price_less_than_500()
        {
            var dbContext = await InMemoryMockDbContext.GetDatabaseContext();
            
            var leadHandlerRepository = new LeadHandlerRepository(dbContext);
 
            var leads = await leadHandlerRepository.GetLeadsWithNewStatus();
            var job = leads.Where(x => (Convert.ToInt32(x.Price) < 500)).First();

            var leadHandler = new AcceptLeadHandler(leadHandlerRepository, CreateLogger< AcceptLeadHandler>());

            var cmd = new AcceptLeadCommand(job.Id);
            var resp = await leadHandler.Handle(cmd, new CancellationToken());

            var leadInDb = await leadHandlerRepository.GetLeadById(job.Id);

            Assert.NotNull(resp);
            Assert.AreEqual(job.Id, resp.JobId);
            Assert.AreEqual(LeadStatusType.Accepted.ToString(), resp.Status);
            Assert.AreEqual(leadInDb.Price, job.Price);
            Assert.AreEqual(LeadStatusType.Accepted.ToString(), leadInDb.Status);
        }

        [Test]
        public async Task Should_Accept_A_New_Lead_With_Price_Greater_than_500()
        {
            var dbContext = await InMemoryMockDbContext.GetDatabaseContext();

            var leadHandlerRepository = new LeadHandlerRepository(dbContext);

            var leads = await leadHandlerRepository.GetLeadsWithNewStatus();
            var job = leads.Where(x => (Convert.ToInt32(x.Price) > 500)).First();

            var leadHandler = new AcceptLeadHandler(leadHandlerRepository, CreateLogger<AcceptLeadHandler>());

            var cmd = new AcceptLeadCommand(job.Id);
            var resp = await leadHandler.Handle(cmd, new CancellationToken());

            var leadInDb = await leadHandlerRepository.GetLeadById(job.Id);

            Assert.NotNull(resp);
            Assert.AreEqual(job.Id, resp.JobId);
            Assert.AreEqual(LeadStatusType.Accepted.ToString(), resp.Status);
            // Assert apply discount of 10%
            Assert.AreEqual(Convert.ToInt32(leadInDb.Price), Convert.ToInt32(Decimal.Multiply(Convert.ToDecimal(job.Price), 0.9m)));
            Assert.AreEqual(LeadStatusType.Accepted.ToString(), leadInDb.Status);
        }
    }
}