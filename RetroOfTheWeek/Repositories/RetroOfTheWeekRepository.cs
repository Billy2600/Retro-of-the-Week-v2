using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;
using RetroOfTheWeek.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace RetroOfTheWeek.Repositories
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class RetroOfTheWeekRepository : IRetroOfTheWeekRepository
    {
        private readonly MySqlConnection _connection;

        public RetroOfTheWeekRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<PostDto> GetPost(int id)
        {
            await _connection.OpenAsync();
            var command = new MySqlCommand($"SELECT `pid`, `title`, `text`, `poster_id`, `date`, `tags`, `img`, `thumb`, `email_author`, `hidden`, `views`, `rating`, `name`, `email` FROM `ret_posts` WHERE `pid` = {id}", _connection);
            var reader = await command.ExecuteReaderAsync();

            var post = new PostDto();

            // Only doing one read, so we can do an if instead of a while
            if(await reader.ReadAsync())
            {
                post.Pid = reader.GetInt32(0);
                post.Title = reader.IsDBNull(1) ? null : reader.GetString(1); // Need to perform this check on nullable DB values
                post.Text = reader.IsDBNull(2) ? null : reader.GetString(2);
                post.PosterId = reader.GetString(3);
                post.Date = reader.GetDateTime(4);
                post.Tags = reader.IsDBNull(5) ? null : reader.GetString(5);
                post.Img = reader.IsDBNull(6) ? null : reader.GetString(6);
                post.Thumb = reader.IsDBNull(7) ? null : reader.GetString(7);
                post.EmailAuthor = reader.GetInt32(8);
                post.Hidden = reader.GetInt32(9);
                post.Views = reader.GetInt32(10);
                post.Rating = reader.GetInt32(11);
                post.Name = reader.GetString(12);
                post.Email = reader.IsDBNull(13) ? null : reader.GetString(13);
            }

            await reader.CloseAsync();
            await _connection.CloseAsync();

            return post;
        }
    }
}
