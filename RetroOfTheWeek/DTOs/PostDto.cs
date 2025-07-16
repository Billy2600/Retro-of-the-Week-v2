using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RetroOfTheWeekAPI.DTOs
{
    [Table("ret_posts")]
    public class PostDto
    {
        [Key]
        [Column("Pid")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        [Column("poster_id")]
        public int PosterId { get; set; }
        public DateTime Date { get; set; }
        public string Tags { get; set; }
        public string Img { get; set; }
        public string Thumb { get; set; }
        [Column("email_author")]
        public int EmailAuthor { get; set; }
        public int Hidden { get; set; }
        public int Views { get; set; }
        public int Rating { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [NotMapped] // Need to do a query to get this
        public int NumComments { get; set; }

        [ForeignKey("PosterId")]
        public virtual UserDto Poster { get; set; } // Virtual enables lazy loading
    }
}
