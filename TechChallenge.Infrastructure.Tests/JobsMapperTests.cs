using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Commands;
using TechChallenge.Domain.Leads.Models;
using TechChallenge.Infrastructure.Mapper;
using TechChallenge.Infrastructure.Repository;

namespace TechChallenge.Infrastructure.Tests
{
    public class JobsMapperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Should_Return_List_of_InvitedLeadsResponse_object_Given_List_of_Jobs_object()
        {
            var dbContext = await InMemoryMockDbContext.GetDatabaseContext();
            var jobs = dbContext.Jobs.ToList();

            var leadResp = JobsMapper.ToInvitedLeadsResponse(jobs);

            Assert.NotNull(leadResp);
            Assert.AreEqual(leadResp.Count, jobs.Count);
            Assert.AreEqual(leadResp[0].Id, jobs[0].Id);
            Assert.AreEqual(leadResp[0].ContactName, jobs[0].ContactName);
            Assert.AreEqual(leadResp[0].CreatedDateTime, jobs[0].CreatedAt.DateTime);
            Assert.AreEqual(leadResp[0].Suburb, jobs[0].Suburb.Name);
            Assert.AreEqual(leadResp[0].CategoryName, jobs[0].Category.Name);
            Assert.AreEqual(leadResp[0].Description, jobs[0].Description);
            Assert.AreEqual(leadResp[0].Price, jobs[0].Price.ToString());
        }

        [Test]
        public void Should_Return_Jobs_object_Given_AddNewLeadCommand_object()
        {
            var leadCmd = new AddNewLeadCommand(1, 1, "_ContactName", "0123456789", "_ContactEmail@example.com", 100, "_Description");

            var job = JobsMapper.ToJobs(leadCmd);
            Assert.NotNull(job);
            Assert.AreEqual(job.Status, LeadStatusType.New.ToString());

            Assert.AreEqual(job.ContactName, leadCmd.ContactName);
            Assert.AreEqual(job.SuburbId, leadCmd.SuburbId);
            Assert.AreEqual(job.CategoryId, leadCmd.CategoryId);
            Assert.AreEqual(job.Description, leadCmd.Description);
            Assert.AreEqual(job.Price, leadCmd.Price);
            Assert.AreEqual(job.ContactEmail, leadCmd.ContactEmail);
            Assert.AreEqual(job.ContactPhone, leadCmd.ContactPhone);
        }

        [Test]
        public async Task Should_Return_List_of_AcceptedLeadsResponse_object_Given_List_of_Jobs_object()
        {
            var dbContext = await InMemoryMockDbContext.GetDatabaseContext();
            var jobs = dbContext.Jobs.ToList();

            var leadResp = JobsMapper.ToAcceptedLeadsResponse(jobs);

            Assert.NotNull(leadResp);
            Assert.AreEqual(leadResp.Count, jobs.Count);
            Assert.AreEqual(leadResp[0].Id, jobs[0].Id);
            Assert.AreEqual(leadResp[0].ContactName, jobs[0].ContactName);
            Assert.AreEqual(leadResp[0].CreatedDateTime, jobs[0].CreatedAt.DateTime);
            Assert.AreEqual(leadResp[0].Suburb, jobs[0].Suburb.Name);
            Assert.AreEqual(leadResp[0].CategoryName, jobs[0].Category.Name);
            Assert.AreEqual(leadResp[0].Description, jobs[0].Description);
            Assert.AreEqual(leadResp[0].Price, jobs[0].Price.ToString());
            Assert.AreEqual(leadResp[0].Postcode, jobs[0].Suburb.Postcode);
        }
        
        [Test]
        public async Task Should_Return_LeadResponse_object_Given_Jobs_object()
        {
            var dbContext = await InMemoryMockDbContext.GetDatabaseContext();
            var jobs = dbContext.Jobs.ToList().First();

            var leadResp = JobsMapper.ToLeadResponse(jobs);

            Assert.NotNull(leadResp);
            Assert.AreEqual(leadResp.Id, jobs.Id);
            Assert.AreEqual(leadResp.ContactName, jobs.ContactName);
            Assert.AreEqual(leadResp.CreatedDateTime, jobs.CreatedAt.DateTime);
            Assert.AreEqual(leadResp.Suburb, jobs.Suburb.Name);
            Assert.AreEqual(leadResp.CategoryName, jobs.Category.Name);
            Assert.AreEqual(leadResp.Description, jobs.Description);
            Assert.AreEqual(leadResp.Price, jobs.Price.ToString());
            Assert.AreEqual(leadResp.Postcode, jobs.Suburb.Postcode);
            Assert.AreEqual(leadResp.Status, jobs.Status);
        }
    }
}