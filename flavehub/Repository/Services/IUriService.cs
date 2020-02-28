using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.Repository.Services
{
     public interface IUriService
    {
        Uri GetPostUri(string postId);
        Uri GetCommentUri(string commentId);
        Uri GetCategoryUri(string categoryId);
    }
}
