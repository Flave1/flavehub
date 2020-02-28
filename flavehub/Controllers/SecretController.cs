using flavehub.Filters;
using Microsoft.AspNetCore.Mvc; 

namespace flavehub.Controllers
{
     
        [ApiKeyAuth]
        [ApiExplorerSettings(IgnoreApi = true)]
        public class SecretController : ControllerBase
        {
            [HttpGet("secret")]
            public IActionResult GetSecret()
            {
                return Ok("You Have a valid ApiKey");
            }
        }
    
}
