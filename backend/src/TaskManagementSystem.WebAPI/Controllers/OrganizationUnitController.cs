using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Application.OrganizationUnitManagement;
using TaskManagementSystem.Application.OrganizationUnitManagement.DTOs;

namespace TaskManagementSystem.WebAPI.Controllers
{
    public class OrganizationUnitController: ApiController
    {
        private readonly IOrganizationUnitService _organizationUnitService;
        private readonly IIdentityUserService _identityUserService;

        public OrganizationUnitController(
            IOrganizationUnitService organizationUnitService,
            IIdentityUserService userService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _organizationUnitService = organizationUnitService;
            _identityUserService = userService;
        }

        [HttpPost("organization")]
        public async Task<IdentityUserDto> CreateOrganization(OrganizationUnitCreateDto input)
        {

            var organizationUnitId = await _organizationUnitService.CreateAsync(input.OrganizationUnitDto);

            var userCreateDto = new UserCreateDto
            {
                IdentityUserDto = input.IdentityUserDto,
                RoleNames = GetAdminRoleNames() ,
                OrganizationUnitId = organizationUnitId
            };
            var user = await SetUserData(userCreateDto);
            return user;
        }

        [HttpPost("organization-user")]
        //[Authorize(Roles = "admin")]
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

        private string[] GetAdminRoleNames()
        {
            List<string> list = new() { Configuration.GetValue<string>("IdentityRole:Administrator:RoleName") };
            List<string> roleNames = list;
            return roleNames.ToArray();
        }

        private string[] GetUserRoleNames()
        {
            List<string> roleNames = new() { Configuration.GetValue<string>("IdentityRole:User:RoleName") };
            return roleNames.ToArray();
        }

        private string GetDefaultPassword() => Configuration.GetValue<string>("IdentityRole:User:DefaultPassword");

        private async Task<IdentityUserDto> SetUserData(UserCreateDto input)
        {
            var user = await _identityUserService.CreateAsync(input.IdentityUserDto);
            await _identityUserService.SetRolesAsync(user.Id, input.RoleNames);
            await _identityUserService.SetOrganizationUnitAsync(user.Id, input.OrganizationUnitId);
            return user;
        }
    }
}
