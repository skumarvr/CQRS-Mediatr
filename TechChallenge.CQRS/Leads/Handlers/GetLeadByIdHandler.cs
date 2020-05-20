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
    public class GetLeadByIdHandler : IRequestHandler<GetLeadByIdQuery, LeadResponse>
    {
        readonly private ILeadHandlerRepository _repository;
        readonly private ILogger<GetLeadByIdHandler> _logger;

        public GetLeadByIdHandler(ILeadHandlerRepository repository,ILogger<GetLeadByIdHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<LeadResponse> Handle(GetLeadByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Get the lead [id:{request.Id}]...");
                var response = await _repository.GetLeadById(request.Id);
                _logger.LogInformation($"Get the lead [id:{request.Id}]");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Faied to get the lead [id:{request.Id}]!!!");
                throw;
            }
}
    }
}
