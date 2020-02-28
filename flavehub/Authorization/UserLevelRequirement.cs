using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.Authorization
{
    public class UserLevelRequirement : IAuthorizationRequirement
    {
        public string UserLevel { get; }
        public UserLevelRequirement(string userLevel)
        {
            UserLevel = userLevel;
        }
    }
}
