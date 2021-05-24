using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetaCinemas.Models
{
    public partial class Room
    {
        public Room()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int RowTotal { get; set; }
        public int ColumnTotal { get; set; }
        public string About { get; set; }

        [ForeignKey("SeatId")]
        public virtual Seat Seat { get; set; }

        [ForeignKey("ShowtimeId")]
        public virtual Showtime Showtime { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
