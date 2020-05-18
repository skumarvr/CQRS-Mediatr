using MediatR;
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

        public AcceptLeadHandler(ILeadHandlerRepository repository)
        {
            _repository = repository;
        }

        public async Task<LeadStatusResponse> Handle(AcceptLeadCommand request, CancellationToken cancellationToken)
        {
            var lead = await _repository.GetLeadById(request.Id);
            if (lead == null) { 
                return new LeadStatusResponse { JobId = request.Id, Status = "" };
            }

            // Check for the price and apply the discount
            var price = Convert.ToDecimal(lead.Price);
            decimal discFactor = 0.9m;
            if( price > 500)
            {
                lead.Price = Decimal.Multiply(price, discFactor).ToString();
            }

            await _repository.UpdateLeadPrice(request.Id, lead.Price);

            var response = await _repository.UpdateLeadStatus(request.Id, LeadStatusType.Accepted.ToString());

            return response;
        }
    }
}
