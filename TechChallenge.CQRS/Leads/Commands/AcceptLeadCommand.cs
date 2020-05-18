using MediatR;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Domain.Leads.Commands
{
    public class AcceptLeadCommand : IRequest<LeadStatusResponse>
    {
        public int Id { get; set; }

        public AcceptLeadCommand(int id)
        {
            this.Id = id;
        }
    }
}
