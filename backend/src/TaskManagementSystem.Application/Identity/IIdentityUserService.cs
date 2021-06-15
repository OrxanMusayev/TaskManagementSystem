using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Domain.Identity.Entities;

namespace TaskManagementSystem.Application.Identity
{
    public interface IIdentityUserService
    {
        Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input);
        Task<List<string>> GetUserEmails(List<Guid> userIds);
        Task<IdentityResult> SetRolesAsync(Guid userId, IdentityUserRolesDto input);
        Task SetOrganizationUnitAsync(Guid userId, Guid ouId);
    }
}
