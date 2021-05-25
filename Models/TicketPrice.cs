using System;
using System.Collections.Generic;

#nullable disable

namespace BetaCinemas.Models
{
    public partial class TicketPrice
    {
        public TicketPrice()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int Weekdays { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int Price { get; set; }
        public bool IsPriority { get; set; }
        public bool Is2D { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
