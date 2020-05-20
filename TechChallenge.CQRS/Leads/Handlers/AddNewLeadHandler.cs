using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Commands;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Domain.Leads.Handlers
{
    public class AddNewLeadHandler : IRequestHandler<AddNewLeadCommand, LeadResponse>
    {
        readonly private ILeadHandlerRepository _repository;
        readonly private ILogger<AddNewLeadHandler> _logger;

        public AddNewLeadHandler(ILeadHandlerRepository repository, ILogger<AddNewLeadHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<LeadResponse> Handle(AddNewLeadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Adding new lead");
                var response = await _repository.AddNewLead(request);
                _logger.LogInformation($"Added new lead");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to Add the lead!!!");
                throw;
            }
        }
    }
}
