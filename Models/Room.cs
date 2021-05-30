using System;
using System.Collections.Generic;

#nullable disable

namespace BetaCinemas.Models
{
    public partial class Room
    {
        public Room()
        {
            Showtimes = new HashSet<Showtime>();
        }

        public int Id { get; set; }
        public int RowTotal { get; set; }
        public int ColumnTotal { get; set; }
        public string About { get; set; }

        public virtual Seat Seat { get; set; }
        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}
