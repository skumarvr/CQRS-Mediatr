using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Models;
using TechChallenge.Infrastructure.Repository;

namespace TechChallenge.Infrastructure.Tests
{
    public class LeadHandlerRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Should_Return_All_Leads_With_New_Status()
        {
            var dbContext = await InMemoryMockDbContext.GetDatabaseContext();
            
            var leadHandlerRepository = new LeadHandlerRepository(dbContext);
            var leads = await leadHandlerRepository.GetLeadsWithNewStatus();

            Assert.NotNull(leads);
            Assert.AreEqual(5, leads.Count);
        }

        [Test]
        public async Task Should_Return_All_Leads_With_Accepted_Status()
        {
            var dbContext = await InMemoryMockDbContext.GetDatabaseContext();

            var leadHandlerRepository = new LeadHandlerRepository(dbContext);
            var leads = await leadHandlerRepository.GetLeadsWithAcceptedStatus();

            Assert.NotNull(leads);
            Assert.AreEqual(3, leads.Count);
        }

        [Test]
        public async Task Should_Update_The_Status_of_the_Lead_To_Accepted()
        {
            var dbContext = await InMemoryMockDbContext.GetDatabaseContext();

            var leadHandlerRepository = new LeadHandlerRepository(dbContext);
            
            var jobToBeUpdated = dbContext.Jobs.First();

            var response = await leadHandlerRepository.UpdateLeadStatus(jobToBeUpdated.Id, LeadStatusType.Accepted.ToString());

            Assert.NotNull(response);
            Assert.AreEqual(LeadStatusType.Accepted.ToString(), response.Status);

            var newLeads = await leadHandlerRepository.GetLeadsWithNewStatus();

            Assert.NotNull(newLeads);
            Assert.AreEqual(4, newLeads.Count);

            var accLeads = await leadHandlerRepository.GetLeadsWithAcceptedStatus();

            Assert.NotNull(accLeads);
            Assert.AreEqual(4, accLeads.Count);
        }

        [Test]
        public async Task Should_Update_The_Status_of_the_Lead_To_Declined()
        {
            var dbContext = await InMemoryMockDbContext.GetDatabaseContext();

            var leadHandlerRepository = new LeadHandlerRepository(dbContext);

            var newLeads = await leadHandlerRepository.GetLeadsWithNewStatus();
            var jobId = newLeads[0].Id;

            var response = await leadHandlerRepository.UpdateLeadStatus(jobId, LeadStatusType.Declined.ToString());

            Assert.NotNull(response);
            Assert.AreEqual(LeadStatusType.Declined.ToString(), response.Status);

            var newLeadsAfterUpdate = await leadHandlerRepository.GetLeadsWithNewStatus();

            Assert.NotNull(newLeadsAfterUpdate);
            Assert.AreEqual(newLeads.Count-1, newLeadsAfterUpdate.Count);
        }
    }
}