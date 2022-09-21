using System;
using System.Collections.Generic;

namespace VideoStore.Console.Models
{
    public partial class Play
    {
        public Play()
        {
            Performances = new HashSet<Performance>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int PayType { get; set; }
        public string? ShortName { get; set; }

        public virtual ICollection<Performance> Performances { get; set; }
    }
}
