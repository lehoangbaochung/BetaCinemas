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

        [Display(Name = "Phòng")]
        public int RoomId { get; set; }

        [Display(Name = "Phim")]
        public int MovieId { get; set; }

        public DateTime Times { get; set; }

        [Display(Name = "Định dạng")]
        public bool Is2D { get; set; }

        [Display(Name = "Suất chiếu đặc biệt")]
        public bool IsSpecial { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

        [Display(Name = "Ngày")]
        public string Date
        {
            get
            {
                var array = Times.ToString().Split(' ')[0].Split('/');
                return $"{ array[2] }/{ array[1] }/20{ array[0] }";
            }
        }

        [Display(Name = "Thời gian")]
        public string Time
        {
            get => Times.ToString().Split(' ')[1];
        }
    }
}
