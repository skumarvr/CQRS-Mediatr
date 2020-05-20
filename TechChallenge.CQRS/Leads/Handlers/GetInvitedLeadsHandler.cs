using MediatR;
using Microsoft.Extensions.Logging;
using System;
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
        readonly private ILogger<GetInvitedLeadsHandler> _logger;

        public GetInvitedLeadsHandler(ILeadHandlerRepository repository, ILogger<GetInvitedLeadsHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<InvitedLeadsResponse>> Handle(GetInvitedLeadsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Get all Invited leads...");
                var response = await _repository.GetLeadsWithNewStatus();
                _logger.LogInformation("Get all Invited leads");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Get all Invited leads!!!");
                throw;
            }
        }
    }
}
