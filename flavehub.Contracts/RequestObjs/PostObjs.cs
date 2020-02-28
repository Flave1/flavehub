using flavehub.Contracts.ApiCore.Response;
using System;
using System.Collections.Generic;

namespace flavehub.Contracts.RequestObjs
{
    public class AddPostReqObj
    { 
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Status { get; set; }
        public string PostBody { get; set; }
        public string PostTitle { get; set; } 
    }

    public class EditPostReqObj
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

    public class PostObj
    {
        public int PostId { get; set; }
        public string UserId { get; set; }  
        public int CategoryId { get; set; } 
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Status { get; set; }
        public string PostBody { get; set; }
        public string PostTitle { get; set; }
        public  IEnumerable<CommentObj> Comments { get; set; }
    }

    public class PostResponse
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Status { get; set; }
        public string PostBody { get; set; }
        public string PostTitle { get; set; }
        public IEnumerable<CommentObj> Comments { get; set; }
    } 
}
