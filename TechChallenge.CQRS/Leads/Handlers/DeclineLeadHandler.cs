using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Commands;
using TechChallenge.Domain.Leads.Models;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Domain.Leads.Handlers
{
    public class DeclineLeadHandler: IRequestHandler<DeclineLeadCommand, LeadStatusResponse>
    {
        readonly private ILeadHandlerRepository _repository;

        public DeclineLeadHandler(ILeadHandlerRepository repository)
        {
            _repository = repository;
        }

        public async Task<LeadStatusResponse> Handle(DeclineLeadCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.UpdateLeadStatus(request.Id, LeadStatusType.Declined.ToString());
            return response;
        }
    }
}
