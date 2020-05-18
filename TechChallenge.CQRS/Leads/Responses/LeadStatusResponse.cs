using System;
using System.Collections.Generic;
using System.Text;

namespace TechChallenge.Domain.Leads.Responses
{
    public class LeadStatusResponse
    {
        public long JobId { get; set; }
        public string Status { get; set; }
    }
}
