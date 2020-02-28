using System;
using System.Collections.Generic; 

namespace flavehub.Contracts.RequestObjs
{
    public class RepliesObj
    {
        public int ReplyId { get; set; }
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public DateTime DateReplied { get; set; }
        public DateTime DateModified { get; set; }
    }
}
