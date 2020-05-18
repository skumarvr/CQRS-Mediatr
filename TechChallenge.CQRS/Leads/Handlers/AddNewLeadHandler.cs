using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Commands;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Domain.Leads.Handlers
{
    public class AddNewLeadHandler : IRequestHandler<AddNewLeadCommand, LeadResponse>
    {
        readonly private ILeadHandlerRepository _repository;

        public AddNewLeadHandler(ILeadHandlerRepository repository)
        {
            _repository = repository;
        }

        public async Task<LeadResponse> Handle(AddNewLeadCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.AddNewLead(request);
            return response;
        }
    }
}
