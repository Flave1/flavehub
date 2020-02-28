using AutoMapper;
using flavehub.Contracts.Commands;
using flavehub.Contracts.RequestObjs;
using flavehub.CustomError;
using flavehub.Domain;
using flavehub.Repository.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace flavehub.Handlers.Posts
{
    public class EditPostHandler : IRequestHandler<EditPostCommand, PostResponse>
    {
        private readonly IPostServices _postServices;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public EditPostHandler(IPostServices postServices, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _mapper = mapper;
            _postServices = postServices;
            _logger = loggerFactory.CreateLogger(typeof(AddPostHandler));

        }
        public async Task<PostResponse> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var post = new Post
                {
                    UserId = request.UserId,
                    Status = request.Status,
                    DateUpdated = DateTime.UtcNow,
                    DatePosted = request.DatePosted,
                    CategoryId = request.CategoryId,
                    PostTitle = request.PostTitle,
                    PostBody = request.PostBody,
                    PostId = request.PostId,
                };
                await _postServices.UpdatePostAsync(post);
                var response = _mapper.Map<PostResponse>(post);
                if(response == null) return await Task.Run(() => new PostResponse());
                return await Task.Run(() => response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Coull not process request", ex);
                throw new InvalidOperationException("Back end mapping", ex);
            }
           
        }
    }
}
