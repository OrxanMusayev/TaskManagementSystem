using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Application.OrganizationUnitManagement.DTOs;

namespace TaskManagementSystem.WebAPI.Controllers
{
    [Authorize]
    public class IdentityController : ApiController
    {
        private readonly IIdentityUserService _identityUserService;

        public IdentityController(IIdentityUserService identityUserService,
                                  IServiceProvider serviceProvider): base(serviceProvider)
        {
            _identityUserService = identityUserService;
        }

        [HttpPost("create-user")]
        [Authorize(Roles = "admin")]
        public async Task<IdentityUserDto> CreateOrganizationUser(OrganizationUnitUserCreateDto input)
        {
            input.IdentityUserDto.Password = GetDefaultPassword();

            var userCreateDto = new UserCreateDto
            {
                IdentityUserDto = input.IdentityUserDto,
                RoleNames = GetUserRoleNames(),
                OrganizationUnitId = input.OrganizationUnitId
            };
            var user = await SetUserData(userCreateDto);
            return user;
        }

        [HttpGet("user-details")]
        public UserDetailsDto GetUserDetails()
        {
            var userId = (Guid) CurrentUserService.UserId;
            return _identityUserService.GetUserDetails(userId);
        }

        private string GetDefaultPassword() => Configuration.GetValue<string>("IdentityRole:User:DefaultPassword");

        private string[] GetUserRoleNames()
        {
            List<string> roleNames = new() { Configuration.GetValue<string>("IdentityRole:User:RoleName") };
            return roleNames.ToArray();
        }

        private async Task<IdentityUserDto> SetUserData(UserCreateDto input)
        {
            var user = await _identityUserService.CreateAsync(input.IdentityUserDto);
            await _identityUserService.SetRolesAsync(user.Id, input.RoleNames);
            await _identityUserService.SetOrganizationUnitAsync(user.Id, input.OrganizationUnitId);
            return user;
        }

    }
}
