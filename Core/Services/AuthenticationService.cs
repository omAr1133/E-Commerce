
global using Domain.Models.Identity;
global using Microsoft.AspNetCore.Identity;
global using Shared.Authentication;
using System.ComponentModel;

namespace Services
{
    internal class AuthenticationService(UserManager<ApplicationUser> userManager)
        : IAuthenticationService
    {
        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            // Check if there is a user with the email address
            var user = await userManager.FindByEmailAsync(request.Email)
                ?? throw new UserNotFoundException(request.Email);

            // Check the password for this user
            var isValid = await userManager.CheckPasswordAsync(user, request.Password);

            if (isValid)
            {
                return new (request.Email,  user.DisplayName,  await CreateTokenAsync(user));
            }

            throw new UnauthorizedException();

        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                DisplayName = request.DisplayName,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName
            };
            var result =await userManager.CreateAsync(user,request.Password);
            if (result.Succeeded) return new(request.Email, user.DisplayName,await CreateTokenAsync(user));
            var errors = result.Errors.Select(e => e.Description).ToList();
            throw new BadRequestException(errors);
        }
        
        private static async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            return "JWTToken";
        }
    }
}
