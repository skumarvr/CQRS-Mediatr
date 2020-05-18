using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechChallenge.Domain.Leads.Commands;
using TechChallenge.Domain.Leads.Events;
using TechChallenge.Domain.Leads.Models;
using TechChallenge.Domain.Leads.Queries;

namespace TechChallenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradieLeadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TradieLeadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("invited")]
        public async Task<IActionResult> GetInvitedLeads()
        {
            var query = new GetInvitedLeadsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("accepted")]
        public async Task<IActionResult> GetAcceptedLeads()
        {
            var query = new GetAcceptedLeadsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLead(int id)
        {
            var query = new GetLeadByIdQuery(id);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddNewLead([FromBody] AddNewLeadCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetLead), new { id = result.Id }, result);
        }

        [HttpPatch("accept/{id}")]
        public async Task<IActionResult> AcceptLead([FromRoute]int id)
        {
            var command = new AcceptLeadCommand(id);
            var result = await _mediator.Send(command);
            if (result.Status == LeadStatusType.Accepted.ToString())
            {
                await _mediator.Publish(new SendEmailEvent("sales@techtest.com", $"Lead {id} is Accepted"));
            }
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPatch("decline/{id}")]
        public async Task<IActionResult> DeclineLead([FromRoute]int id)
        {
            var command = new DeclineLeadCommand(id);
            var result = await _mediator.Send(command);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }
    }
}