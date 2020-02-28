using AutoMapper;
using flavehub.Contracts.Queries;
using flavehub.Contracts.RequestObjs;
using flavehub.Repository.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace flavehub.Handlers.Posts
{
    public class GetPostByIdHandler : IRequestHandler<GetPostByIdQuery, PostResponse>
    {
        private readonly IPostServices _postServices;
        private readonly IMapper _mapper;
        public GetPostByIdHandler(IPostServices postServices, IMapper mapper)
        {
            _mapper = mapper;
            _postServices = postServices;
        }
        public async Task<PostResponse> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postServices.GetPostByIdAsync(request.PostId);
            return post == null ? null : _mapper.Map<PostResponse>(post);
        }
    }
}
