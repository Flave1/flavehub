using flavehub.Contracts.RequestObjs;
using flavehub.Contracts.V1;
using flavehub.Data;
using flavehub.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace flavehub.IntegrationTest
{
     public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient TestClient;
        private readonly IServiceProvider _serviceProvider;
        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DataContext));
                        services.AddDbContext<DataContext>(option => option.UseInMemoryDatabase("TestDatabase"));
                    });
                });
           _serviceProvider = appFactory.Services;
            TestClient = appFactory.CreateClient();
        }

        
        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<PostResponse> CreatePostAsync(AddPostReqObj reqObj)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Posts.Create, reqObj);
            return await response.Content.ReadAsAsync<PostResponse>();
        }

        protected async Task<PostResponse> UpdatePostAsync(EditPostReqObj reqObj)
        {  
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Posts.Update.Replace("{postId}", reqObj.PostId.ToString()), reqObj);
             return await  response.Content.ReadAsAsync<PostResponse>();
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationReqObj { 
                Email = "Flave@Integration.com",
                Password = "Flave@Password123"
            });
            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
            context.Database.EnsureDeleted();
        }

    }
}
