using RetroOfTheWeekAPI.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetroOfTheWeekAPI.DTOs
{
    [Table("ret_comments")]
    public class CommentDto
    {
        [Key]
        [Column("Cid")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        [Column("poster_id")]
        public int PosterId { get; set; }
        public DateTime Date { get; set; }
        [Column("post_id")]
        public int PostId { get; set; }
        public int Reply { get; set; }
        [Column("ip_address")]
        public string IpAddress { get; set; }
        [Column("msg_reply")]
        public int MsgReply { get; set; }

        [ForeignKey("PosterId")]
        public virtual UserDto Poster { get; set; } // Virtual enables lazy loading
    }
}
