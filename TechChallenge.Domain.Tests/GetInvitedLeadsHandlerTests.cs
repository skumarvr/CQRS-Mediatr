using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Models;
using TechChallenge.Infrastructure.Repository;
using TechChallenge.Infrastructure.Tests;

namespace TechChallenge.Domain.Tests
{
    public class GetInvitedLeadsHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Should_Return_All_New_Leads()
        {
            var dbContext = await InMemoryMockDbContext.GetDatabaseContext();

            var leadHandlerRepository = new LeadHandlerRepository(dbContext);

            var leads = await leadHandlerRepository.GetLeadsWithNewStatus();

            var leadInDb = dbContext.Jobs.ToList().Where(x => x.Status == LeadStatusType.New.ToString()).ToList();

            Assert.NotNull(leads);
            Assert.AreEqual(leads.Count, leadInDb.Count);
        }

    }
}
