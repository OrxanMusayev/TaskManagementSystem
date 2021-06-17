using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Identity.Entities;
using TaskManagementSystem.Domain.OrganizationUnitManagement.Entities;

namespace TaskManagementSystem.Domain.Identity
{
    public interface IIdentityUserManager
    {
        Task<ApplicationUser> CreateAsync(ApplicationUser input, string password);
        Task<List<string>> GetEmailsById(List<Guid> ids);
        Task<List<Claim>> GetClaimsAsync(ApplicationUser user);
        Task<List<string>> GetRolesAsync(ApplicationUser user);
        IQueryable<ApplicationUser> GetUsers();
        IQueryable<ApplicationUser> GetById(Guid userId);
        Task<SignInResult> SignInAsync(string userName, string password, bool rememberMe);
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByIdAsync(Guid id);
        Task<IdentityResult> AddToRolesAsync(Guid id, string[] roles);
        Task<IdentityResult> AddClaimsAsync(ApplicationUser user, Claim[] claims);
        Task AddToOrganizationUnitAsync(Guid userId, Guid ouId);
        Task AddToOrganizationUnitAsync(ApplicationUser user, OrganizationUnit ou);
    }
}
