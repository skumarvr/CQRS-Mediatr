using MediatR;
using Microsoft.Extensions.Logging;
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
        readonly private ILogger<GetAcceptedLeadsHandler> _logger;
        readonly private ILeadHandlerRepository _repository;

        public GetAcceptedLeadsHandler(ILeadHandlerRepository repository, ILogger<GetAcceptedLeadsHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<AcceptedLeadsResponse>> Handle(GetAcceptedLeadsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Get all Accepted leads...");
                var response = await _repository.GetLeadsWithAcceptedStatus();
                _logger.LogInformation("Get all Accepted leads");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Get all Accepted leads!!!");
                throw;
            }
        }
    }
}
