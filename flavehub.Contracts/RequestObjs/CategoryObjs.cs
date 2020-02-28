using System;
using System.Collections.Generic; 

namespace flavehub.Contracts.RequestObjs
{
    public class AddCategoryReqObj
    {
        public string CategoryName { get; set; }
    }
    public class EditCategoryReqObj
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class CategoryObj
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class CategoryResponseObj
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
