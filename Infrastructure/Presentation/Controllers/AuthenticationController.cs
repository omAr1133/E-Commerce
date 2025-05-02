using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Authentication;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager serviceManager)
    : APIController
    {
        //[HttpPost]
        // Login (LoginRequest{string email , string Password})
        // => UserResponse {string Token , string Email , string DisplayName}

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest request)
            => Ok( await serviceManager.AuthenticationService.LoginAsync(request));

        //[HttpPost]
        // Register(RegisterRequest{string Email , string UserName, string Password , string DisplayName })
        // => UserResponse {string Token , string Email , string DisplayName}

        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest request)
            => Ok( await serviceManager.AuthenticationService.RegisterAsync(request));
    }
}
