using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace flavehub.Domain
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Commented { get; set; }
        public int PostId { get; set; } 
        public string UserId { get; set; }
        public DateTime DateCommented { get; set; }
        public DateTime DateModified { get; set; }
        public virtual ICollection<Replies> Replies { get; set; }

    }
}
