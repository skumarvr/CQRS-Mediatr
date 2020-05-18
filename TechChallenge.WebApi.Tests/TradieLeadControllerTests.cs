using NUnit.Framework;
using System.Threading.Tasks;
using TechChallenge.Infrastructure.Tests;

namespace TechChallenge.WebApi.Tests
{
    public class TradieLeadControllerTests
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