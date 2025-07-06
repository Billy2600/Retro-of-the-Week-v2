using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroOfTheWeek.Models;
using RetroOfTheWeek.Repositories;
using RetroOfTheWeekShared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetroOfTheWeekAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        private readonly IRetroOfTheWeekRepository _repo;
        private readonly IMapper _mapper;

        public CommentsController(IRetroOfTheWeekRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Post/{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostComments(int postId)
        {
            var comments = await _repo.GetPostComments(postId);

            return comments == null ? NotFound() : Ok(_mapper.Map<List<CommentModel>>(comments));
        }
    }
}
