using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetroOfTheWeek.Models;
using RetroOfTheWeek.Repositories;
using AutoMapper;

namespace RetroOfTheWeek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController
    {
        private readonly IRetroOfTheWeekRepository _repo;
        private readonly IMapper _mapper;

        public PostsController(IRetroOfTheWeekRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<PostModel> Get(int id)
        {
            var post = await _repo.GetPost(id);
            return _mapper.Map<PostModel>(post);
        }

        [HttpGet]
        [Route("Latest/{count}/{pagebreak:bool=false}")]
        public async Task<List<PostModel>> GetLatestPosts(int count, bool pagebreak)
        {
            var posts = await _repo.GetLatestPosts(count, pagebreak);
            return _mapper.Map<List<PostModel>>(posts);
        }
    }
}
