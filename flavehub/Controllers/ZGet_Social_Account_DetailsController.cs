using flavehub.Contracts.SocialAccount;
using flavehub.Contracts.V1;
using flavehub.CustomError;
using flavehub.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace flavehub.Controllers
{ 
    public class ZGet_Social_Account_DetailsController : ControllerBase
    {

        private readonly IGithubService _githubService;
        public ZGet_Social_Account_DetailsController(IGithubService githubService)
        {
            _githubService = githubService;
        }

        [HttpGet(ApiRoutes.Social.Github)]
        public async Task<IActionResult> GethubDetailsByUserName(GithubReqObj user)
        {
            try
            {
                var userProfile = await _githubService.GetUserProfile(user.Username);
                return Ok(userProfile);
            }catch(Exception message)
            {
                return NotFound( new HttpRequestExceptionError(message.Message));
            }
           
        }
    }
}
