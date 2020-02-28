using flavehub.Contracts.RequestObjs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace flavehub.Contracts.Queries
{
    public class GetAllCategorQuery : IRequest<List<CategoryResponseObj>> { }

    public class GetCategorByIdQuery : IRequest<CategoryResponseObj>
    {
        public int CategoryId { get; }
        public GetCategorByIdQuery(int categoryId) { CategoryId = categoryId; }
    }
    public class DeleteCategorByIdQuery : IRequest<bool>
    {
        public int CategoryId { get; }
        public DeleteCategorByIdQuery(int categoryId) { CategoryId = categoryId; }
    }
}
