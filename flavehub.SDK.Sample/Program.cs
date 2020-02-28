using flavehub.Contracts.RequestObjs;
using flavehub.Sdk;
using Refit;
using System;
using System.Threading.Tasks;

namespace flavehub.SDK.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cachedToken = string.Empty;

            var identityApi = RestService.For<IIdentityApi>("https://localhost:5001");
            var flavehubApi = RestService.For<IflavehubApi>("https://localhost:5001", new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken)
            });

            var loginResponse = await identityApi.LoginAsync(new UserLoginReqObj
            {
                Email = "sdkaccount@gmail.com",
                Password = "Test1234!"
            });

            var registerResponse = await identityApi.RegisterAsync(new UserRegistrationReqObj
            {
                Email = "sdkaccount@gmail.com",
                Password = "Test1234!"
            });

           

            cachedToken = loginResponse.Content.Token;

            var allPosts = await flavehubApi.GetAllAsync();

            var createdPost = await flavehubApi.CreateAsync(new AddPostReqObj
            {
                PostTitle = "This is created by the SDK",
                PostBody = "This is created by the SDK",
                CategoryId = 2,
                DatePosted = DateTime.Now,
                DateUpdated = DateTime.Now,
                Status = 1, 
                UserId = "SDK User"
            });
            if (createdPost.Content == null) return;

            var retrievedPost = await flavehubApi.GetAsync(createdPost.Content.PostId);

            var updatedPost = await flavehubApi.UpdateAsync(createdPost.Content.PostId, new EditPostReqObj
            {
                PostTitle = "This is Edited by the SDK",
                PostBody = "This is Edited by the SDK",
                CategoryId = 2,
                DatePosted = DateTime.Now,
                DateUpdated = DateTime.Now,
                Status = 1,
                UserId = "SDK User",
                PostId = createdPost.Content.PostId
            });

            var deletePost = await flavehubApi.DeleteAsync(createdPost.Content.PostId);
        }
    }
}
