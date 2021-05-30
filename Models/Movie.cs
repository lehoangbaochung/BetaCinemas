using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetaCinemas.Models
{
    [Display(Name = "Phim")]
    public partial class Movie
    {
        public Movie()
        {
            Showtimes = new HashSet<Showtime>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Tựa đề")]
        public string Title { get; set; }

        [Display(Name = "Giới thiệu chi tiết")]
        public string About { get; set; }

        [Display(Name = "Thời lượng")]
        public int? Duration { get; set; }

        [Display(Name = "Đạo diễn")]
        public string Director { get; set; }

        [Display(Name = "Diễn viên")]
        public string Actor { get; set; }

        [Display(Name = "Quốc gia")]
        public string Country { get; set; }

        [Display(Name = "Ngôn ngữ")]
        public string Lang { get; set; }

        [Display(Name = "Thể loại")]
        public string Genre { get; set; }

        [Display(Name = "Thời gian khởi chiếu")]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Địa chỉ hình ảnh áp phích")]
        public string PosterUrl { get; set; }

        [Display(Name = "Địa chỉ video trailer")]
        public string TrailerUrl { get; set; }

        [Display(Name = "Trạng thái khởi chiếu")]
        public bool? IsShowing { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        [Display(Name = "Giới thiệu")]
        public string Description => About.Length > 100 ? About.Remove(100).TrimEnd() + "..." : About;

        [Display(Name = "Ngày khởi chiếu")]
        public string ReleaseDate2
        {
            get
            {
                var array = ReleaseDate.ToString().Split(' ')[0].Split('/');
                return $"{ array[2] }/{ array[1] }/20{ array[0] }";
            }
        }

        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}