using flavehub.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.Repository.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri; 
        }

        public Uri GetCategoryUri(string categoryId)
        {
            return new Uri(_baseUri + ApiRoutes.Category.Get.Replace("{categoryId}", categoryId));
        }

        public Uri GetCommentUri(string commentId)
        {
            return new Uri(_baseUri + ApiRoutes.Comment.Get.Replace("{commentId}", commentId));
        }

        public Uri GetPostUri(string postId)
        {
            return new Uri(_baseUri + ApiRoutes.Posts.Get.Replace("{postId}", postId));
        }
    }
}
