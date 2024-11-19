using Api.Model;
using Api.Services;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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

        /// <summary>
        /// get all post
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        { 
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        /// <summary>
        /// get post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// create post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost("create")]
        //[Authorize()]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            try
            {
                Post result = await _postService.CreatePostAsync(post);
                string message = "le post a été ajoutée avec succès";
                return Ok(new { message, result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// delete post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {

            try
            {
                Post result = await _postService.DeletePostAsync(id);
                string message = "le post a été supprime avec succès";
                return Ok(new { message, result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
