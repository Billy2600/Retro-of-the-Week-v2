using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetroOfTheWeekAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using RetroOfTheWeekAPI.DTOs;
using RetroOfTheWeekShared.Models;

namespace RetroOfTheWeekAPI.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _repo.GetPost(id);

            return post == null ? NotFound() : Ok(_mapper.Map<PostModel>(post));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Latest/{count}/{pagebreak:bool=false}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PostModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLatestPosts(int count, bool pagebreak)
        {
            var posts = await _repo.GetLatestPosts(count, pagebreak);
            return posts.Count == 0 ? NotFound() : Ok(_mapper.Map<List<PostModel>>(posts));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPost([FromBody] PostModel post)
        {
            if (post == null)
            {
                throw new ArgumentNullException();
            }

            var newPost = await _repo.AddPost(_mapper.Map<PostDto>(post));
            return Ok(_mapper.Map<PostModel>(newPost));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePost([FromQuery] int id)
        {
            if(id == 0)
            {
                throw new ArgumentException("Id cannot be zero");
            }

            await _repo.DeletePost(id);

            return NoContent();
        }
    }
}
