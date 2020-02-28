using flavehub.Contracts.RequestObjs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace flavehub.Contracts.Queries
{
    public class GetAllPostsQuery : IRequest<List<PostResponse>> { }

    public class GetPostByIdQuery : IRequest<PostResponse> 
    {
        public int PostId { get; }
        public GetPostByIdQuery(int postId)  { PostId = postId; }
    }
    public class DeletePostByIdQuery : IRequest<bool> 
    { 
        public int PostId { get; }
        public DeletePostByIdQuery(int postId)  { PostId = postId; }
    }
}
