using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flavehub.Data;
using flavehub.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace flavehub.Repository.Services
{
    public class PostServices : IPostServices
    {
        private readonly DataContext _dataContext;  

        public PostServices(DataContext context )
        {
            _dataContext = context; 
        }
        public async Task<bool> CreatePostAsync(Post post)
        { 
                await _dataContext.Posts.AddAsync(post); 
                var created = await _dataContext.SaveChangesAsync();

                return created > 0;
          
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            var post = await GetPostByIdAsync(postId);
            _dataContext.Remove(post);

            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
           var post = await _dataContext.Posts.Include(x=>x.Comments).SingleOrDefaultAsync(x => x.PostId == postId);
            return post;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            try
            {
                var queryable = _dataContext.Posts.AsQueryable();
                var psts = await queryable.Include(x => x.Comments).ToListAsync();
                return psts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdatePostAsync(Post post)
        { 
                var postToUpdate = await GetPostByIdAsync(post.PostId);
                _dataContext.Entry(postToUpdate).CurrentValues.SetValues(post);

                var updated = await _dataContext.SaveChangesAsync();
                return updated > 0;
            
        }

        public async Task<bool> UserOwnsPostAsync(int postId, string userId)
        {
            var post = await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.PostId == postId);
            if (post == null)  return false;

            if (post.UserId != userId)  return false; 
            return true;
        }
    }
}
