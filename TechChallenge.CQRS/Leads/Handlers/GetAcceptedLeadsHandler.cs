using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Queries;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Domain.Leads.Handlers
{
    public class GetAcceptedLeadsHandler : IRequestHandler<GetAcceptedLeadsQuery, List<AcceptedLeadsResponse>>
    {
        readonly private ILeadHandlerRepository _repository;

        public GetAcceptedLeadsHandler(ILeadHandlerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AcceptedLeadsResponse>> Handle(GetAcceptedLeadsQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetLeadsWithAcceptedStatus();
            return response;
        }
    }
}
