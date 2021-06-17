using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Application.OrganizationUnitManagement.DTOs;
using TaskManagementSystem.Domain.Identity.Entities;

namespace TaskManagementSystem.Application.Identity
{
    public interface IIdentityUserService
    {
        Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input);
        Task<SignInResult> SignInAsync(LoginInput input);
        Task<ApplicationUser> FindByNameAsync(string userName);
        UserDetailsDto GetUserDetails(Guid userId);
        Task<List<string>> GetUserEmails(List<Guid> userIds);
        Task<List<Claim>> GetClaimsAsync(ApplicationUser input);
        Task<List<string>> GetRolesAsync(ApplicationUser input);
        Task<IdentityResult> SetRolesAsync(Guid userId, string[] roles);
        Task SetOrganizationUnitAsync(Guid userId, Guid ouId);
        Task<IdentityResult> SetClaimsAsync(ApplicationUser input, Claim[] claims);
    }
}
