using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Domain.Leads.Commands
{
    public class DeclineLeadCommand : IRequest<LeadStatusResponse>
    {
        public int Id { get; set; }

        public DeclineLeadCommand(int id)
        {
            this.Id = id;
        }
    }
}
