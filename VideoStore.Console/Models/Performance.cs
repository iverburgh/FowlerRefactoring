using System;
using System.Collections.Generic;

namespace VideoStore.Console.Models
{
    public partial class Performance
    {
        public Guid Id { get; set; }
        public Guid PlayId { get; set; }
        public int Audience { get; set; }
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Play Play { get; set; } = null!;
    }
}
