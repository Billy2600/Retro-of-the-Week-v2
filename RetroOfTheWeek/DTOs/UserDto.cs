using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RetroOfTheWeekAPI.DTOs
{
    [Table("ret_users")]
    public class UserDto
    {
        [Key]
        [Column("Uid")]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string AboutMe { get; set; }
        [Column("account_type")]
        public int AccountType { get; set; }
        [Column("join_date")]
        public DateTime JoinDate { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public int Gender { get; set; }
        public string Country { get; set; }
        public string Skype { get; set; }
        public string MSN { get; set; }
        public string Yahoo { get; set; }
        public string AIM { get; set; }
        public string Steam { get; set; }
    }
}
