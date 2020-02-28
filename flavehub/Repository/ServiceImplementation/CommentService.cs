using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flavehub.Data;
using flavehub.Domain;
using Microsoft.EntityFrameworkCore;

namespace flavehub.Repository.Services
{
    public class CommentService : ICommentService  
    {
        private DataContext _dataContext;
        public CommentService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateCommentAsync(Comment comment)
        {  
                await _dataContext.AddAsync(comment);
                var created = await _dataContext.SaveChangesAsync();
                return created > 0;
            
        }

        public async  Task<bool> DeleteCommentAsync(int CommnentId)
        { 
                var post = await GetCommentByIdAsync(CommnentId); 
                 _dataContext.Remove(post); 
                var deleted = await _dataContext.SaveChangesAsync(); 
                return deleted > 0;
          
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            return await _dataContext.Comments.SingleOrDefaultAsync(x => x.CommentId == commentId);
        }

        public async Task<List<Comment>> GetCommentsAsync()
        { 
               return await _dataContext.Comments.ToListAsync(); 
        }

        public async Task<bool> UpdateCommentAsync(Comment comment)
        {
            _dataContext.Update(comment);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> UserOwnsCommentAsync(int commentId, string userId)
        {
            var comment = await _dataContext.Comments.AsNoTracking().SingleOrDefaultAsync(x => x.CommentId == commentId);
            if (comment == null)
                return false;
            if (comment.UserId != userId)
                return false; 
            return true;

        }
    }
}
