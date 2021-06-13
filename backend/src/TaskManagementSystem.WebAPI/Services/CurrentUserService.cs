using System;
using System.Linq;
using TaskManagementSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace TaskManagementSystem.Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            if (Guid.TryParse(GetHeaderValue(httpContextAccessor, "X-UserId"), out Guid userId))
            {
                UserId = userId;
            }
        }

        public Guid? UserId { get; }

        private string GetHeaderValue(IHttpContextAccessor httpContextAccessor, string headerKey)
        {
            if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.Request.Headers.TryGetValue(headerKey, out StringValues headerValues))
            {
                return headerValues.First();
            }

            return string.Empty;
        }
    }
}
