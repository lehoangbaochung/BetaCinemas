using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetaCinemas.Models
{
    [Display(Name = "Suất chiếu")]
    public partial class Showtime
    {
        public Showtime()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        public int RoomId { get; set; }

        public int MovieId { get; set; }

        [Display(Name = "Thời gian")]
        public DateTime ShowTime { get; set; }

        [Display(Name = "Định dạng")]
        public bool Is2D { get; set; }

        [Display(Name = "Suất chiếu đặc biệt")]
        public bool IsSpecial { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
