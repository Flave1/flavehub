using flavehub.Contracts.SocialAccount;
using flavehub.CustomError;
using flavehub.Repository.Services;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace flavehub.Repository.ServiceImplementation
{
    public class GithubService : IGithubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AsyncRetryPolicy<GithubUser> _retryPolicy;
        private const int MaxRetries = 3;
        public GithubService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _retryPolicy = Policy<GithubUser>.Handle<HttpRequestException>()
                .WaitAndRetryAsync(MaxRetries, times => 
                TimeSpan.FromMilliseconds(times * 100));

            //ex => {   return ex.Message != "Fake Url";  }
            //.WaitAndRetryAsync(MaxRetries, times => TimeSpan.FromSeconds(1));
        }
        public async Task<GithubUser> GetUserProfile(string username)
        {
            var client = _httpClientFactory.CreateClient("GitHub");

            Random random = new Random();
            return await _retryPolicy.ExecuteAsync(async () => 
            {
                if(random.Next(1,3) == 1) 
                    throw new HttpRequestException("Try Again"); 

                var result = await client.GetAsync($"users/{username}");
                if(result.StatusCode == HttpStatusCode.NotFound) { return null; }

                var accountInfo = await result.Content.ReadAsStringAsync();
                return  JsonConvert.DeserializeObject<GithubUser>(accountInfo);
            });
        }
    }
}
