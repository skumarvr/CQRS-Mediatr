using MediatR;
using Microsoft.Extensions.Logging;
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
        readonly private ILogger<DeclineLeadHandler> _logger;

        public DeclineLeadHandler(ILeadHandlerRepository repository, ILogger<DeclineLeadHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<LeadStatusResponse> Handle(DeclineLeadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Declining the lead [id:{request.Id}]...");
                var response = await _repository.UpdateLeadStatus(request.Id, LeadStatusType.Declined.ToString());
                _logger.LogInformation($"Declined the lead [id:{request.Id}]...");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to Decline the lead [id:{request.Id}]!!!");
                throw;
            }
        }
    }
}
