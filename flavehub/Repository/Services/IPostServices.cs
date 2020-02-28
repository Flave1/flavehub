using flavehub.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.Repository.Services
{
    public interface IPostServices
    {
        Task<List<Post>> GetPostsAsync();
        Task<Post> GetPostByIdAsync(int postId);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(int postId);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> UserOwnsPostAsync(int postId, string userId); 
    }
}
