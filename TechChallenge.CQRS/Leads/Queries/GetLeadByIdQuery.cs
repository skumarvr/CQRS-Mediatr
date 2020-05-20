using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Domain.Leads.Queries
{
    public class GetLeadByIdQuery : IRequest<LeadResponse>
    {
        public int Id { get; set; }

        public GetLeadByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
