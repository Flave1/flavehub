using System;
using System.Collections.Generic; 

namespace flavehub.Contracts.RequestObjs
{
    public class AddCommentReqObj
    {
        public int CommentId { get; set; }
        public string Commented { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public DateTime DateCommented { get; set; }
        public DateTime DateModified { get; set; } 
    }

    public class EditCommentReqObj
    {
        public int CommentId { get; set; }
        public string Commented { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public DateTime DateCommented { get; set; }
        public DateTime DateModified { get; set; } 
    }

    public class CommentObj
    {
        public int CommentId { get; set; }
        public string Commented { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public DateTime DateCommented { get; set; }
        public DateTime DateModified { get; set; }
        public virtual ICollection<RepliesObj> Replies { get; set; }
    }

    public class CommentResponse
    {
        public int CommentId { get; set; }
        public string Commented { get; set; }
        public int PostId { get; set; }
        public DateTime DateCommented { get; set; }
        public DateTime DateModified { get; set; }
        public List<CommentObj> Comments { get; set; }
    }
}
