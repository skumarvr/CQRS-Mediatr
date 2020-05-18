using System;
using System.Collections.Generic;

namespace TechChallenge.Data.EntityModels
{
    public partial class Jobs
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int SuburbId { get; set; }
        public int CategoryId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Suburbs Suburb { get; set; }
    }
}
