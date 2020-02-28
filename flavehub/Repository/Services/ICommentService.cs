using flavehub.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.Repository.Services
{
    public interface ICommentService
    {
        Task<List<Comment>> GetCommentsAsync();
        Task<Comment> GetCommentByIdAsync(int commentId);
        Task<bool> UpdateCommentAsync(Comment comment  );
        Task<bool> DeleteCommentAsync(int CommnentId);
        Task<bool> CreateCommentAsync(Comment comment  );
        Task<bool> UserOwnsCommentAsync(int commentId, string userId);
    }
}
