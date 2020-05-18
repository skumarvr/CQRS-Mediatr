using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechChallenge.Data;
using TechChallenge.Data.EntityModels;
using TechChallenge.Domain.Leads;
using TechChallenge.Domain.Leads.Commands;
using TechChallenge.Domain.Leads.Models;
using TechChallenge.Domain.Leads.Responses;
using TechChallenge.Infrastructure.Mapper;

namespace TechChallenge.Infrastructure.Repository
{
    public class LeadHandlerRepository : ILeadHandlerRepository
    {
        private readonly hipagesDbContext _context;
        public LeadHandlerRepository(hipagesDbContext context)
        {
            _context = context;
        }

        public async Task<LeadResponse> AddNewLead(AddNewLeadCommand request)
        {
            var newJob = JobsMapper.ToJobs(request);
            _context.Jobs.Add(newJob);
            await _context.SaveChangesAsync();
            return JobsMapper.ToLeadResponse(newJob);
        }

        public async Task<LeadResponse> GetLeadById(int id)
        {
            var jobs = await _context.Jobs.Where(s => s.Id == id)
                                    .Include(x => x.Suburb)
                                    .Include(x => x.Category)
                                    .FirstOrDefaultAsync();
            if (jobs == null) return null;                
            return JobsMapper.ToLeadResponse(jobs);
        }

        public async Task<List<AcceptedLeadsResponse>> GetLeadsWithAcceptedStatus()
        {
            var jobs = await _context.Jobs
                                    .Where(s => s.Status == LeadStatusType.Accepted.ToString())
                                    .Include(x => x.Suburb)
                                    .Include(x => x.Category)
                                    .ToListAsync();

            return JobsMapper.ToAcceptedLeadsResponse(jobs);
        }

        public async Task<List<InvitedLeadsResponse>> GetLeadsWithNewStatus()
        {
            var jobs = await _context.Jobs
                                    .Where(x => x.Status == LeadStatusType.New.ToString())
                                    .Include(x => x.Suburb)
                                    .Include(x => x.Category)
                                    .ToListAsync();

            return JobsMapper.ToInvitedLeadsResponse(jobs);
        }

        public async Task<LeadStatusResponse> UpdateLeadStatus(int id, string status)
        {
            var job = await _context.Jobs.Where(s => s.Id == id)
                                    .FirstOrDefaultAsync();
            if (job != null)
            {
                job.Status = status;
                _context.Update(job);
                await _context.SaveChangesAsync();
                return new LeadStatusResponse { JobId = id, Status = status };
            }

            return new LeadStatusResponse { JobId = id, Status = "" };
        }
    }
}
