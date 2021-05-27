using System;

namespace BetaCinemas.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public DateTime PostTime { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AttachedUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Genre { get; set; }

        public string Description 
            => Content.Length > 100 ? Content.Remove(100) + "..." : Content;

        public string PostDate
        {
            get
            {
                var array = PostTime.ToString().Split(' ')[0].Split('/');
                return $"{ array[2] }/{ array[1] }/20{ array[0] }";
            }
        }
    }
}
