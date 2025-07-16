

namespace RetroOfTheWeekShared.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int PostId { get; set; }
        public int Reply { get; set; }
        public string IpAddress { get; set; }
        public int MsgReply { get; set; }

        public UserModel Poster { get; set; }
    }
}
