using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Commands;
using TechChallenge.Domain.Leads.Models;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Domain.Leads.Handlers
{
    public class AcceptLeadHandler : IRequestHandler<AcceptLeadCommand, LeadStatusResponse>
    {
        readonly private ILeadHandlerRepository _repository;

        public AcceptLeadHandler(ILeadHandlerRepository repository)
        {
            _repository = repository;
        }

        public async Task<LeadStatusResponse> Handle(AcceptLeadCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.UpdateLeadStatus(request.Id, LeadStatusType.Accepted.ToString());
            return response;
        }
    }
}
