using flavehub.Contracts.ApiCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace flavehub.Domain
{

    [NotMapped]
    public class ApplicationUser : IdentityUser
    { 
        public string Firstname { get; set; }
        public string Lastname { get; set; } 
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public UserLevel UserLevel { get; set; }

        
    }
}
