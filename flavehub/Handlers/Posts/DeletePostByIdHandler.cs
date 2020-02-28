using AutoMapper;
using flavehub.Contracts.Queries;
using flavehub.Contracts.RequestObjs;
using flavehub.Repository.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace flavehub.Handlers.Posts
{

    public class DeletePostByIdHandler : IRequestHandler<DeletePostByIdQuery, bool>
    {
        private readonly IPostServices _postServices;
        private readonly ILogger _logger;
        public DeletePostByIdHandler(IPostServices postServices, ILoggerFactory loggerFactory)
        {
            _postServices = postServices;
            _logger = loggerFactory.CreateLogger(typeof(DeletePostByIdHandler));
        }

        Task<bool> IRequestHandler<DeletePostByIdQuery, bool>.Handle(DeletePostByIdQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                var post = _postServices.GetPostByIdAsync(request.PostId);

                if (post.Result != null)
                {
                    var deleted = _postServices.DeletePostAsync(request.PostId); 
                    if (deleted.Result)   return Task.Run(() => true); 
                } 
                return Task.Run(() => false);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Unable to process request", ex.InnerException.Message);
                throw new InvalidOperationException("Back end issue", ex);
            }
        }
    }
}
