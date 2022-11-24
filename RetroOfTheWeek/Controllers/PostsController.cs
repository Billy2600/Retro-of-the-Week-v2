using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetroOfTheWeek.Models;
using RetroOfTheWeek.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace RetroOfTheWeek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _repo.GetPost(id);

            return post == null ? NotFound() : Ok(_mapper.Map<PostModel>(post));
        }

        [HttpGet]
        [Route("Latest/{count}/{pagebreak:bool=false}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PostModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLatestPosts(int count, bool pagebreak)
        {
            var posts = await _repo.GetLatestPosts(count, pagebreak);
            return posts.Count == 0 ? NotFound() : Ok(_mapper.Map<List<PostModel>>(posts));
        }
    }
}
