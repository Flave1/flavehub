using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flavehub.Domain
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public int CategoryId { get; set; }

        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Status { get; set; }
        public string PostBody { get; set; }
        public string PostTitle { get; set; } 
        public virtual List<Comment> Comments { get; set; }
    }
}
