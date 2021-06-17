using Microsoft.AspNetCore.Http;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInput input)
        {
            var user = await _userService.FindByNameAsync(input.UserName);
            if (user == null)
            {
                return BadRequest(new AuthenticationResult
                {
                    Error = "User not found"
                });
            }

            var signInResult = await _userService.SignInAsync(input);

            if (signInResult.Succeeded)
            {
                var claims = await _userService.GetClaimsAsync(user);
                if (claims == null || claims.Count == 0)
                {
                    claims = await AddClaims(user);
                }

                var token = await _tokenService.GenerateJSONWebToken(input, claims.ToArray());

                return Ok(token);
            }
            return Unauthorized(new AuthenticationResult
            {
                Error = "Invalid user name or password"
            });
        }

        [HttpPut("logout")]
        public async Task Logout()
        {
            HttpContext.Session.Clear();
        }


        private async Task<List<Claim>> AddClaims(ApplicationUser user)
        {
            List<Claim> claims;
            var roles = await _userService.GetRolesAsync(user);

            claims = new()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
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
