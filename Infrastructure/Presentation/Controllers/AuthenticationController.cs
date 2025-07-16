﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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

        //[HttpGet]
        //CheckEmail(string email) => bool
        [HttpGet("emailexists")]
        public async Task <ActionResult<bool>> CheckEmail(string email)
        {
            return Ok(await serviceManager.AuthenticationService.CheckEmailAsync(email));
        }

        // GetCurrentUserAddress() => AddressDTO
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok( await serviceManager.AuthenticationService.GetUserAddressAsync(email!));
        }

        // UpdateCurrentUserAddress(AddressDTO) => AddressDTO
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.AuthenticationService.UpdateUserAddressAsync(addressDTO, email!));
        }

        // GetCurrentUser (string Token , string Email , string DisplayName)
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.AuthenticationService.GetUserByEmail(email!));
        }
    }
}
