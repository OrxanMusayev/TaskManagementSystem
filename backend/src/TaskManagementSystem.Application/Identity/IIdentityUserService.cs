using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Domain.Identity.Entities;

namespace TaskManagementSystem.Application.Identity
{
    public interface IIdentityUserService
    {
        Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input);
        Task<SignInResult> SignInAsync(LoginInput input);
        Task<IdentityUserDto> FindByNameAsync(string userName);
        Task<List<string>> GetUserEmails(List<Guid> userIds);
        Task<List<Claim>> GetClaimsAsync(IdentityUserDto input);
        Task<List<string>> GetRolesAsync(IdentityUserDto input);

        Task<IdentityResult> SetRolesAsync(Guid userId, string[] roles);
        Task SetOrganizationUnitAsync(Guid userId, Guid ouId);
        Task<IdentityResult> SetClaimsAsync(IdentityUserDto input, Claim[] claims);
    }
}
