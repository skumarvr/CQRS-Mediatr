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
    public class GetLeadByIdHandler : IRequestHandler<GetLeadByIdQuery, LeadResponse>
    {
        readonly private ILeadHandlerRepository _repository;

        public GetLeadByIdHandler(ILeadHandlerRepository repository)
        {
            _repository = repository;
        }

        public async Task<LeadResponse> Handle(GetLeadByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetLeadById(request.id);
            return response;
        }
    }
}
