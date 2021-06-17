using System;
using System.Linq;
using TaskManagementSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace TaskManagementSystem.Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserId = GetUserId(httpContextAccessor);
        }

        public Guid? UserId { get; }

        private Guid GetUserId(IHttpContextAccessor httpContextAccessor)
        {
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var claims = claimsIdentity.FindAll(ClaimTypes.NameIdentifier);
            if (claims.Count()!=0)
            {
                return Guid.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}
