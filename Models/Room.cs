using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetaCinemas.Models
{
    public partial class Room
    {
        public Room()
        {
            Showtimes = new HashSet<Showtime>();
        }

        public int Id { get; set; }

        [Display(Name = "Tổng hàng")]
        public int RowTotal { get; set; }

        [Display(Name = "Tổng cột")]
        public int ColumnTotal { get; set; }

        [Display(Name = "Thời gian gửi")]
        public string About { get; set; }

        public virtual Seat Seat { get; set; }
        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}
