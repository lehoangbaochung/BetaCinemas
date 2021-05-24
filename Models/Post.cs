using System;

namespace BetaCinemas.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public DateTime PostTime { get; set; }
        public string Content { get; set; }
        public string AttachedUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}
