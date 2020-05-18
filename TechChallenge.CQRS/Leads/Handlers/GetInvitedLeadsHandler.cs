using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Queries;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Domain.Leads.Handlers
{
    public class GetInvitedLeadsHandler : IRequestHandler<GetInvitedLeadsQuery, List<InvitedLeadsResponse>>
    {
        readonly private ILeadHandlerRepository _repository;

        public GetInvitedLeadsHandler(ILeadHandlerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InvitedLeadsResponse>> Handle(GetInvitedLeadsQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetLeadsWithNewStatus();
            return response;
        }
    }
}
