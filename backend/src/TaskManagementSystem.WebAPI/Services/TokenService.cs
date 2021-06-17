using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Domain.Identity.Entities;

namespace TaskManagementSystem.WebAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IConfiguration configuration,
                            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<AuthenticationResult> GenerateJSONWebToken(LoginInput input, Claim[] claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:ValidIssuer"],
              _configuration["Jwt:ValidAudience"],
              claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: credentials);


            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

            if (!string.IsNullOrEmpty(tokenHandler))
            {
                _httpContextAccessor.HttpContext.Session.SetString("JWTToken", tokenHandler);
            }
            return new AuthenticationResult
            {
                AccessToken = tokenHandler,
                TokenType = _configuration["Jwt:TokenType"]
            };
        }
    }
}