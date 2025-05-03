
namespace Services
{
    internal class AuthenticationService(UserManager<ApplicationUser> userManager,
        IOptions<JWTOptions> options)
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

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var jwt = options.Value;
            var claims = new List<Claim>()
            {
             new(type: ClaimTypes.Email, user.Email!),
             new(type: ClaimTypes.Name, user.UserName!)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(item: new( ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(jwt.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(jwt.DurationInDays),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

    }
}
