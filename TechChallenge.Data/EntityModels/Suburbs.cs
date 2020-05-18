using System;
using System.Collections.Generic;

namespace TechChallenge.Data.EntityModels
{
    public partial class Suburbs
    {
        public Suburbs()
        {
            Jobs = new HashSet<Jobs>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Postcode { get; set; }

        public virtual ICollection<Jobs> Jobs { get; set; }
    }
}
