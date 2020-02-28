using flavehub.Contracts.RequestObjs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace flavehub.Contracts.Commands
{
    public class AddCommentCommand : IRequest<CommentResponse>
    {
        public int CommentId { get; set; }
        public string Commented { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public DateTime DateCommented { get; set; }
        public DateTime DateModified { get; set; }
    }
}
