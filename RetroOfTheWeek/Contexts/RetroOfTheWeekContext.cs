using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using RetroOfTheWeek.DTOs;
using RetroOfTheWeekAPI.DTOs;

namespace RetroOfTheWeek.Contexts
{
    public class RetroOfTheWeekContext : DbContext
    {
        // Virtual so they can be mocked for unit tests
        public virtual DbSet<PostDto> Posts { get; set; }
        public virtual DbSet<UserDto> Users { get; set; }
        public virtual DbSet<CommentDto> Comments { get; set; }

        public RetroOfTheWeekContext(DbContextOptions<RetroOfTheWeekContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
