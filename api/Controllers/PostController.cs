using Api.Model;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("get")]
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            await _postService.CreatePostAsync(post);
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

    

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }
    }
}
