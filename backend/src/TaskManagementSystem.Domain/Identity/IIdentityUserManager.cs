using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Identity.Entities;
using TaskManagementSystem.Domain.OrganizationUnitManagement.Entities;

namespace TaskManagementSystem.Domain.Identity
{
    public interface IIdentityUserManager
    {
        Task<ApplicationUser> CreateAsync(ApplicationUser input, string password);
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByIdAsync(Guid id);
        Task<IdentityResult> AddToRolesAsync(Guid id, string[] roles);
        Task AddToOrganizationUnitAsync(Guid userId, Guid ouId);
        Task AddToOrganizationUnitAsync(ApplicationUser user, OrganizationUnit ou);
    }
}
