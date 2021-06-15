using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Domain.Identity;
using TaskManagementSystem.Domain.Identity.Entities;

namespace TaskManagementSystem.Application.Identity
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly IIdentityUserManager _userManager;
        private readonly IMapper _mapper;

        public IdentityUserService(IIdentityUserManager userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            var user = new ApplicationUser(
                Guid.NewGuid(),
                input.UserName,
                input.Email,
                input.Name,
                input.Surname
            );

            var result = await _userManager.CreateAsync(user, input.Password);
            return _mapper.Map<ApplicationUser, IdentityUserDto>(result);
        }

        public async Task<IdentityResult> SetRolesAsync(Guid userId, IdentityUserRolesDto input)
        {
            var result = await _userManager.AddToRolesAsync(userId, input.RoleNames);
            return result;
        }

        public async Task SetOrganizationUnitAsync(Guid userId, Guid ouId)
        {
            await _userManager.AddToOrganizationUnitAsync(userId, ouId);
        }
    }
}
