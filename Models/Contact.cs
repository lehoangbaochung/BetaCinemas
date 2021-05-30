using System;
using System.ComponentModel.DataAnnotations;

namespace BetaCinemas.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string MemberId { get; set; }

        public DateTime SentTime { get; set; }

        [Display(Name = "Nội dung gửi")]
        public string SentContent { get; set; }

        public DateTime? ReplyTime { get; set; }

        [Display(Name = "Nội dung trả lời")]
        public string ReplyContent { get; set; }

        [Display(Name = "Trạng thái")]
        public bool IsReplied { get; set; }

        public virtual Member Member { get; set; }

        [Display(Name = "Thời gian gửi")]
        public string SentTimeToString
        {
            get
            {
                var array = SentTime.ToString().Split(' ')[0].Split('/');
                return $"{ array[2] }/{ array[1] }/20{ array[0] } { SentTime.ToString().Split(' ')[1] }";
            }
        }

        [Display(Name = "Thời gian trả lời")]
        public string ReplyTimeToString
        {
            get
            {
                if (ReplyTime.HasValue == false) return string.Empty;

                var array = ReplyTime.ToString().Split(' ')[0].Split('/');
                return $"{ array[2] }/{ array[1] }/20{ array[0] } { SentTime.ToString().Split(' ')[1] }";
            }
        }
    }
}
