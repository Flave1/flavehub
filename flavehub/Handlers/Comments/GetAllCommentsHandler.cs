using AutoMapper;
using flavehub.Contracts.Queries;
using flavehub.Contracts.RequestObjs;
using flavehub.Repository.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace flavehub.Handlers.Comments
{
    public class GetAllCommentsHandler : IRequestHandler<GelAllCommentQuery, List<CommentResponse>>
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        public GetAllCommentsHandler(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }
       
        async Task<List<CommentResponse>> IRequestHandler<GelAllCommentQuery, List<CommentResponse>>.Handle(GelAllCommentQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentService.GetCommentsAsync();
            return _mapper.Map<List<CommentResponse>>(comments);
        }
    }
}
