using Api.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task CreatePostAsync(Post post);
        Task DeletePostAsync(int id);
    }

    public class PostService : IPostService
    {
        private readonly Context _context;

        public PostService(Context context)
        {
            _context = context;
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
        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Post.FindAsync(id);
        }



        /// <summary>
        /// create post by users
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task CreatePostAsync(Post post)
        {
            post.CreateDate = DateTime.Now;
            post.UpdateDate = DateTime.Now;
            await _context.Post.AddAsync(post);
            await _context.SaveChangesAsync();
        }
    

        /// <summary>
        /// supprime un post par un users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       
        public async Task DeletePostAsync(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post != null)
            {
                _context.Post.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}
