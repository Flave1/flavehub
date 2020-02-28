using AutoMapper;
using flavehub.Contracts.Commands;
using flavehub.Contracts.RequestObjs;
using flavehub.Domain;
using flavehub.Repository.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks; 

namespace flavehub.Handlers.Comments
{
    public class AddCommentHandler : IRequestHandler<AddCommentCommand, CommentResponse>
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public AddCommentHandler(ICommentService commentService, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _commentService = commentService;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger(typeof(AddCommentHandler));
        }
        public async Task<CommentResponse> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var comment = new Comment
                {
                    UserId = request.UserId,
                    DateModified = DateTime.UtcNow,
                    DateCommented = request.DateCommented,
                    PostId = request.PostId,
                    Commented = request.Commented,
                };
                if (!await _commentService.CreateCommentAsync(comment))
                {
                    throw new NotImplementedException("Coull not process request");
                }
                return _mapper.Map<CommentResponse>(comment);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Coull not process request", ex);
                throw new NotImplementedException("Internal error", ex);
            }
      
        }
    }
}
