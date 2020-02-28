using AutoMapper;
using Castle.Core.Logging;
using flavehub.Contracts.Queries;
using flavehub.Contracts.RequestObjs;
using flavehub.Controllers;
using flavehub.Data;
using flavehub.Domain;
using flavehub.Handlers.Posts;
using flavehub.Repository.Services;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace flavehubAPI.Moq
{
    public class PostServiceTest
    {
        private readonly GetPostByIdHandler _sut1; 
        private readonly Mock<IMapper> _mapperMoq = new Mock<IMapper>(); 
        private readonly Mock<IPostServices> _postServiceMoq = new Mock<IPostServices>();
        public PostServiceTest()
        {
            _sut1 = new GetPostByIdHandler(_postServiceMoq.Object, _mapperMoq.Object );
        }

        [Fact]
        public async Task GetPostByIdAsync_ShouldReturnCustomer_WhenCustomerExist()
        {
            //arrange
            int PostId = 1;
            var userId = "37057f22-0104-4dde-a255-1808b3c8beaa";
            var postBody = "This the integration test post  body";
            var postTitle = "The Integration test post tile";
            
            CancellationToken cancellationToken;

            var postDTO = new Post
            {
                CategoryId = 1,
                DatePosted = DateTime.Now,
                DateUpdated = DateTime.Now,
                PostBody = postBody,
                PostTitle = postTitle,
                Status = 1,
                UserId = userId,
                PostId = PostId
            };

            
              _postServiceMoq.Setup(x => x.GetPostByIdAsync(PostId)).ReturnsAsync(postDTO);
            //Act

            var query =  new GetPostByIdQuery(postDTO.PostId);
            var post = await _sut1.Handle(query, cancellationToken);

            //Assert

            Assert.Equal(PostId.ToString(), post.PostId.ToString());
        }
    }
}
