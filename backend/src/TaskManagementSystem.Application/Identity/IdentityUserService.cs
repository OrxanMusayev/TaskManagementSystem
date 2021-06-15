using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Domain.Identity;
using TaskManagementSystem.Domain.Identity.Entities;

namespace TaskManagementSystem.Application.Identity
{
    public class IdentityUserService: IIdentityUserService
    {
        private readonly IIdentityUserManager _userManager;

        public IdentityUserService(IIdentityUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> CreateAsync(IdentityUserCreateDto input)
        {
            var user = new ApplicationUser(
                Guid.NewGuid(),
                input.UserName,
                input.Email,
                input.Name, 
                input.Surname
            );

            var result = await _userManager.Register(user, input.Password);
            return result;
        }
    }
}
