using flavehub.Contracts.RequestObjs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace flavehub.Contracts.Queries
{
    public class GelAllCommentQuery : IRequest<List<CommentResponse>> {  }

}
