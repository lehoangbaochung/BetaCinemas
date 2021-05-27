using System;
using System.Collections.Generic;

namespace BetaCinemas.Models
{
    public partial class Showtime
    {
        public Showtime()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime ShowTime { get; set; }
        public bool Is2D { get; set; }

        public virtual Room IdNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
