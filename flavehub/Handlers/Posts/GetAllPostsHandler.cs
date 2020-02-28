using AutoMapper;
using flavehub.Contracts.Queries;
using flavehub.Contracts.RequestObjs;
using flavehub.Repository.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace flavehub.Handlers.Posts
{
    public class GetAllPostsHandler : IRequestHandler<GetAllPostsQuery, List<PostResponse>>
    {
        private readonly IPostServices _postServices;
        private readonly IMapper _mapper;
        public GetAllPostsHandler(IPostServices postServices, IMapper mapper)
        {
            _mapper = mapper;
            _postServices = postServices;
        }

        public async Task<List<PostResponse>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postServices.GetPostsAsync();
            return _mapper.Map<List<PostResponse>>(posts);
        }
    }
}
