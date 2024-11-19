using Api.Model;
using Api.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;

namespace Api.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int postId);
        Task<Post> CreatePostAsync(Post post);
        Task<Post> DeletePostAsync(int postId);
    }

    public class PostService : IPostService
    {
        private readonly IConnectionService _connectionService;
        private readonly IHttpContextAccessor _httpContextAccessor;       
        private readonly Context _context;

        public PostService(Context context, IConnectionService connectionService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _connectionService = connectionService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// get  all post
        /// </summary>
        /// <returns></returns>
        /// sauf si les utlise est en mod eprive ne montre pas ses postes
        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Post.ToListAsync();
        }

        /// <summary>
        /// get post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///  sauf si les utlise est en mod eprive ne montre pas ses postes
        public async Task<Post> GetPostByIdAsync(int postId)
        {
            Post post = await _context.Post.FindAsync(postId);

            if (post != null)
            {
                return post;
            }
            else
            {
                throw new ArgumentException("L'action a échoué");
            }
        }


        /// <summary>
        /// create post by users
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<Post> CreatePostAsync(Post post)
        {
            //var userInfo = _connectionService.GetCurrentUserInfo(_httpContextAccessor);
            //int userId = userInfo.Id;
 
            // if (userId == 0)
            //    throw new ArgumentException("L'action a échoué : l'utilisateur n'existe pas");*/
           
            post.CreateDate = DateTime.Now;
            post.UpdateDate = DateTime.Now;
            await _context.Post.AddAsync(post);
            await _context.SaveChangesAsync();

            Post postCreate = await _context.Post.FindAsync(post.Id);

            if(postCreate != null)
            {
                return postCreate;
            }
            else
            {
                throw new ArgumentException("L'action a échoué");
            }
        }
    

        /// <summary>
        /// delete post by users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Post> DeletePostAsync(int postId)
        {
            //var userInfo = _connectionService.GetCurrentUserInfo(_httpContextAccessor);
            //int userId = userInfo.Id;

            // if (userId == 0)
            //    throw new ArgumentException("L'action a échoué : l'utilisateur n'existe pas");*/

            Post post = await _context.Post.FindAsync(postId);
            if (post != null)
            {
                _context.Post.Remove(post);
                await _context.SaveChangesAsync();
                return post;
            }
            else
            {
                throw new ArgumentException("L'action a échoué");
            }
        }
    }
}
