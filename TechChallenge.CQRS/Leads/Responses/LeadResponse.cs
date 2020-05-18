using System;
using System.Collections.Generic;
using System.Text;

namespace TechChallenge.Domain.Leads.Responses
{
    public class LeadResponse
    {
        public long Id { get; set; }
        public string ContactName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Suburb { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Postcode { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
    }
}
