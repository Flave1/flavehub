using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 

namespace flavehub.Domain
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } 
    }
}
