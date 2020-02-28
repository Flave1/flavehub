using flavehub.Contracts.RequestObjs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace flavehub.Contracts.Commands
{
    
    public class AddCategoryCommand : IRequest<CategoryResponseObj>
    {
        public string CategoryName { get; set; }
    }
    public class EditCategoryCommand : IRequest<CategoryResponseObj>
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
