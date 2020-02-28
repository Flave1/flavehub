using flavehub.Contracts.SocialAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.Repository.Services
{
    public interface IGithubService
    {
        Task<GithubUser> GetUserProfile(string username);
        
    }
}
