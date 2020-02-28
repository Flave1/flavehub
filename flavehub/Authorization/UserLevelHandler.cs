using flavehub.Contracts.ApiCore;
using flavehub.CustomError;
using flavehub.Domain;
using flavehub.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace flavehub.Authorization
{
    public class UserLevelHandler : AuthorizationHandler<UserLevelRequirement>
    {

        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        public UserLevelHandler(ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }
        protected  override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserLevelRequirement requirement)
        {
            var userId = context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = context.User?.FindFirstValue(ClaimTypes.Name);
            var userlevel = context.User?.FindFirstValue("level");
            if (userlevel != null && userlevel.ToString() == requirement.UserLevel.ToString())
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            } 
            context.Fail();
            return Task.CompletedTask;

        }
    }
}
