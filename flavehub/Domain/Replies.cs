using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.Domain
{
    public class Replies
    {
        [Key]
        public int ReplyId { get; set; }
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public DateTime DateReplied { get; set; }
        public DateTime DateModified { get; set; }
    }
}
