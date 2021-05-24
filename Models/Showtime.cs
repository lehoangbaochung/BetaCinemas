using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetaCinemas.Models
{
    public partial class Showtime
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime ShowTime1 { get; set; }
        public string Format { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
    }
}
