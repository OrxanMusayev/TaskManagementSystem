using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Domain.Identity.Entities;

namespace TaskManagementSystem.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<AuthenticationResult> GenerateJSONWebToken(LoginInput input, Claim[] claims);
    }
}
