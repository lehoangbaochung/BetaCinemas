using System;
using System.ComponentModel.DataAnnotations;

namespace BetaCinemas.Models
{
    public partial class Post
    {
        public int Id { get; set; }

        [Display(Name = "Thời gian")]
        public DateTime PostTime { get; set; }

        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [Display(Name = "Chủ đề")]
        public string Genre { get; set; }

        [Display(Name = "URL đính kèm")]
        public string AttachedUrl { get; set; }

        [Display(Name = "URL hình ảnh")]
        public string ImageUrl { get; set; }

        [Display(Name = "Mô tả")]
        public string Description
            => Content.Length > 100 ? Content.Remove(100).TrimEnd() + "..." : Content;

        [Display(Name = "Ngày đăng")]
        public string PostDate
        {
            get
            {
                var array = PostTime.ToString().Split(' ')[0].Split('/');
                return $"{ array[2] }/{ array[1] }/20{ array[0] }";
            }
        }

        [Display(Name = "Thời gian")]
        public string PostTimeToString
        {
            get
            {
                var array = PostTime.ToString().Split(' ')[0].Split('/');
                return $"{ array[2] }/{ array[1] }/20{ array[0] } { PostTime.ToString().Split(' ')[1] }";
            }
        }
    }
}