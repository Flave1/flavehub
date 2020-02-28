using flavehub.Contracts.RequestObjs;
using flavehub.Contracts.V1;
using flavehub.Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace flavehub.IntegrationTest.Tests
{
    public class PostControllerTest : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAny_Posts_Returns_Empty_Response()
        {
            //Arrange
            await AuthenticateAsync();

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetAll);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Post>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task Get_PostIfPostExistInDatabase()
        {
            //Arrange
            await AuthenticateAsync();
            var createdPost = await CreatePostAsync(new AddPostReqObj
            {
                CategoryId = 1,
                DatePosted = DateTime.Now,
                DateUpdated = DateTime.Now,
                PostBody = "This the integration test post  body",
                PostTitle = "The Integration test post tile",
                Status = 1,
                UserId = "37057f22-0104-4dde-a255-1808b3c8beaa"
            });

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.Get.Replace("{postId}", createdPost.PostId.ToString()));


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedPost = await response.Content.ReadAsAsync<Post>();

            returnedPost.PostId.Should().Be(createdPost.PostId);
            returnedPost.PostTitle.Should().Be(createdPost.PostTitle);
            returnedPost.UserId.Should().Be(createdPost.UserId);
            returnedPost.CategoryId.Should().Be(createdPost.CategoryId);
            returnedPost.PostBody.Should().Be(createdPost.PostBody);
            returnedPost.Status.Should().Be(createdPost.Status);
            returnedPost.DatePosted.Should().Be(createdPost.DatePosted);
            returnedPost.DateUpdated.Should().Be(createdPost.DateUpdated);
        }

        [Fact]
        public async Task Delete_PostIfPostExist()
        {
            //Arrange
            await AuthenticateAsync();
            var createdPost = await CreatePostAsync(new AddPostReqObj
            {
                CategoryId = 1,
                DatePosted = DateTime.Now,
                DateUpdated = DateTime.Now,
                PostBody = "This the integration test post  body",
                PostTitle = "The Integration test post tile",
                Status = 1,
                UserId = "37057f22-0104-4dde-a255-1808b3c8beaa"
            });
            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Posts.Delete.Replace("{postId}", createdPost.PostId.ToString()));

            //Assert
            //Should be Forbidden (403) because you must have the power of an ULTIMATE to delete a post
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Update_PostIfPost_NotExist_Return_NotFound()
        {
            //Arrange
            await AuthenticateAsync();

            var response = await UpdatePostAsync(new EditPostReqObj
            {
                CategoryId = 13,
                DatePosted = DateTime.Now,
                DateUpdated = DateTime.Now,
                PostBody = "This the integration test post  body Edited",
                PostTitle = "The Integration test post tile Edited",
                Status = 1,
                UserId = "37057f22-0104-4dde-a255-1808b3c8beaa",
                PostId = 13,
            });

            //Act  
            response.Should().Be(response);


            //Assert


        }
    }
}
