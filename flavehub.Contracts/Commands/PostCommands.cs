using flavehub.Contracts.RequestObjs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace flavehub.Contracts.Commands
{
     public class AddPostCommand : IRequest<PostResponse>
    {
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Status { get; set; }
        public string PostBody { get; set; }
        public string PostTitle { get; set; }
    }

    public class EditPostCommand : IRequest<PostResponse>
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Status { get; set; }
        public string PostBody { get; set; }
        public string PostTitle { get; set; }
    }
}
