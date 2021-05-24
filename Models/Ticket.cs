using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetaCinemas.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public int Price { get; set; }
        public DateTime SoldTime { get; set; }
        public bool IsAdult { get; set; }
        public string MemberId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
    }
}
