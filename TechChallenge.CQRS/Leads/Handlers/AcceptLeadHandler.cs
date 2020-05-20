using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Commands;
using TechChallenge.Domain.Leads.Models;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Domain.Leads.Handlers
{
    public class AcceptLeadHandler : IRequestHandler<AcceptLeadCommand, LeadStatusResponse>
    {
        readonly private ILeadHandlerRepository _repository;
        readonly private ILogger<AcceptLeadHandler> _logger;

        public AcceptLeadHandler(ILeadHandlerRepository repository, ILogger<AcceptLeadHandler> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<LeadStatusResponse> Handle(AcceptLeadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Accepting the lead [id:{request.Id}]...");
                var lead = await _repository.GetLeadById(request.Id);
                if (lead == null)
                {
                    return new LeadStatusResponse { JobId = request.Id, Status = "" };
                }

                // Check for the price and apply the discount
                var price = Convert.ToDecimal(lead.Price);
                decimal discFactor = 0.9m;
                if (price > 500)
                {
                    _logger.LogInformation($"Updating the price for the lead [id:{request.Id}]...");
                    lead.Price = Decimal.Multiply(price, discFactor).ToString();
                    await _repository.UpdateLeadPrice(request.Id, lead.Price);
                    _logger.LogInformation($"Updated the price for the lead [id:{request.Id}]");
                }

                var response = await _repository.UpdateLeadStatus(request.Id, LeadStatusType.Accepted.ToString());
                _logger.LogInformation($"Accepted the lead [id:{request.Id}]");
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Failed to Accept the lead [id:{request.Id}]!!!");
                throw;
            }
        }
    }
}
