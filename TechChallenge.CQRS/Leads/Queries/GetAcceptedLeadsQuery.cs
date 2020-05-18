using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TechChallenge.Domain.Leads.Responses;

namespace TechChallenge.Domain.Leads.Queries
{
    public class GetAcceptedLeadsQuery:IRequest<List<AcceptedLeadsResponse>>
    {
    }
}
