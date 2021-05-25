using System;
using System.Collections.Generic;

namespace BetaCinemas.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            BillDetails = new HashSet<BillDetail>();
        }

        public int Id { get; set; }
        public string MemberId { get; set; }
        public int MovieId { get; set; }
        public int ShowtimeId { get; set; }
        public int TicketPriceId { get; set; }
        public DateTime SoldTime { get; set; }

        public virtual Member Member { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Showtime Showtime { get; set; }
        public virtual TicketPrice TicketPrice { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
