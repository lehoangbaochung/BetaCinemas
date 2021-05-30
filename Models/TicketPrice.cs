using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetaCinemas.Models
{
    [Display(Name = "Giá vé")]
    public partial class TicketPrice
    {
        public TicketPrice()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        [Display(Name = "Loại")]
        public string DateType { get; set; }

        [Display(Name = "Bắt đầu")]
        public TimeSpan? StartTime { get; set; }

        [Display(Name = "Kết thúc")]
        public TimeSpan? EndTime { get; set; }

        [Display(Name = "Giá (VND)")]
        public int Price { get; set; }

        [Display(Name = "Đối tượng")]
        public bool IsPriority { get; set; }

        [Display(Name = "Định dạng")]
        public bool Is2D { get; set; }

        [Display(Name = "Suất chiếu đặc biệt")]
        public bool IsSpecial { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}