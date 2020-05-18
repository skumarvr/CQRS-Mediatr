using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Commands;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Domain.Leads
{
    public interface ILeadHandlerRepository
    {
        Task<List<InvitedLeadsResponse>> GetLeadsWithNewStatus();
        Task<List<AcceptedLeadsResponse>> GetLeadsWithAcceptedStatus();
        Task<LeadStatusResponse> UpdateLeadStatus(int id, string status);
        Task UpdateLeadPrice(int id, string price);
        Task<LeadResponse> GetLeadById(int id);
        Task<LeadResponse> AddNewLead(AddNewLeadCommand request);
    }
}
