using RetroOfTheWeek.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroOfTheWeek.Repositories
{
    public interface IRetroOfTheWeekRepository
    {
        public Task<PostDto> GetPost(int id);
    }
}
