using System;
using System.Collections.Generic;

#nullable disable

namespace BetaCinemas.Models
{
    public partial class BillDetail
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public int TicketId { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
