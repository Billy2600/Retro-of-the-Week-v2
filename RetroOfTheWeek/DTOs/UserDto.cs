using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetroOfTheWeek.DTOs
{
    public class UserDto
    {
        [Key]
        public int Uid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string AboutMe { get; set; }
        public int AccountType { get; set; }
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
