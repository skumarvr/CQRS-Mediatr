using System;
using System.Collections.Generic;

namespace TechChallenge.Data.EntityModels
{
    public partial class Categories
    {
        public Categories()
        {
            Jobs = new HashSet<Jobs>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }

        public virtual ICollection<Jobs> Jobs { get; set; }
    }
}
