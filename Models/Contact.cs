using System;

namespace BetaCinemas.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string MemberId { get; set; }
        public DateTime SentTime { get; set; }
        public string Content { get; set; }
        public bool IsReplied { get; set; }

        public virtual Member Member { get; set; }
    }
}
