using System.ComponentModel.DataAnnotations;

namespace BetaCinemas.Models
{
    public partial class Seat
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        [Display(Name = "Chỉ mục hàng")]
        public string RowIndex { get; set; }

        [Display(Name = "Chỉ mục cột")]
        public int ColumnIndex { get; set; }

        [Display(Name = "Trạng thái đặt chỗ")]
        public bool IsEmpty { get; set; }

        [Display(Name = "Vị trí")]
        public string Index
        {
            get => RowIndex + ColumnIndex;
        }

        public virtual Room Room { get; set; }
    }
}
