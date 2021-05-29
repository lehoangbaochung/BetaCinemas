using System;
using System.ComponentModel.DataAnnotations;

namespace BetaCinemas.Models
{
    public partial class Contact
    {
        public int Id { get; set; }

        [Display(Name = "Email thành viên")]
        public string MemberId { get; set; }

        [Display(Name = "Thời gian gửi")]
        public DateTime SentTime { get; set; }

        [Display(Name = "Nội dung gửi")]
        public string SentContent { get; set; }

        [Display(Name = "Thời gian trả lời")]
        public DateTime ReplyTime { get; set; }

        [Display(Name = "Nội dung trả lời")]
        public string ReplyContent { get; set; }

        public bool IsReplied { get; set; }       

        public virtual Member Member { get; set; }
    }
}
