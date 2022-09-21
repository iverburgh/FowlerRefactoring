using System;
using System.Collections.Generic;

namespace VideoStore.Console.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Performances = new HashSet<Performance>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Performance> Performances { get; set; }
    }
}
