using System;
using System.Collections.Generic;
using System.Text;

namespace TechChallenge.Domain.Leads.Models
{
    public class Lead
    {
        public int Id { get; set; }
        public int SuburbId { get; set; }
        public int CategoryId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}
