using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Application.Identity;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Application.OrganizationUnitManagement;
using TaskManagementSystem.Application.OrganizationUnitManagement.DTOs;
using TaskManagementSystem.Domain.Common.Exceptions;
using TaskManagementSystem.Domain.Identity.Entities;

namespace TaskManagementSystem.WebAPI.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IIdentityUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IIdentityUserService userService,
            ITokenService tokenService,
            IServiceProvider serviceProvider): base(serviceProvider)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet("login")]
        public async Task<AuthenticationResult> Login(LoginInput input)
        {
            var user = await _userService.FindByNameAsync(input.UserName);
            if (user == null)
            {
                throw new UserFriendlyException("User not found");
            }

            var signInResult = await _userService.SignInAsync(input);

            if (signInResult.Succeeded)
            {
                var claims = await _userService.GetClaimsAsync(user);
                if (claims == null)
                {
                    claims = await AddClaims(user);
                }

                var token = await _tokenService.GenerateJSONWebToken(input, claims.ToArray());

                return token;
            }
            throw new UserFriendlyException("Invalid user name or password");
        }

        [HttpPut("logout")]
        public async Task Logout()
        {
            HttpContext.Session.Clear();
        }


        private async Task<List<Claim>> AddClaims(IdentityUserDto user)
        {
            List<Claim> claims;
            var roles = await _userService.GetRolesAsync(user);

            claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            await _userService.SetClaimsAsync(user, claims.ToArray());
            return claims;
        }
    }
}
