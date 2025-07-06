using System;

namespace RetroOfTheWeek.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int PosterId { get; set; }
        public DateTime? Date { get; set; }
        public string Tags { get; set; }
        public string Img { get; set; }
        public string Thumb { get; set; }
        public int EmailAuthor { get; set; }
        public int Hidden { get; set; }
        public int? Views { get; set; }
        public int? Rating { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UserModel Poster { get; set; }
    }
}
