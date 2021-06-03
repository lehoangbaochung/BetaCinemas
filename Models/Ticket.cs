using System;
using System.ComponentModel.DataAnnotations;

namespace BetaCinemas.Models
{
    [Display(Name = "Vé")]
    public partial class Ticket
    {
        public int Id { get; set; }
        public string MemberId { get; set; }
        public int ShowtimeId { get; set; }

        [Display(Name = "Giá vé (VND)")]
        public int TicketPrice{ get; set; }

        [Display(Name = "Thời gian đặt vé")]
        public DateTime SoldTime { get; set; }

        public virtual Member Member { get; set; }

        [Display(Name = "Suất chiếu")]
        public virtual Showtime Showtime { get; set; }

        [Display(Name = "Ngày")]
        public string Date
        {
            get
            {
                var array = SoldTime.ToString().Split(' ')[0].Split('/');
                return $"{ array[2] }/{ array[1] }/20{ array[0] }";
            }
        }

        [Display(Name = "Thời gian")]
        public string Time
        {
            get => SoldTime.ToString().Split(' ')[1];
        }

        [Display(Name = "Thời gian")]
        public string DateTime
        {
            get => $"{ Date } { Time }";
        }
    }
}
