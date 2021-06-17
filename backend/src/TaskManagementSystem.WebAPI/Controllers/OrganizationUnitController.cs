using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class OrganizationUnitController : ApiController
    {
        private readonly IOrganizationUnitService _organizationUnitService;
        private readonly IIdentityUserService _identityUserService;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public OrganizationUnitController(
            IOrganizationUnitService organizationUnitService,
            IIdentityUserService userService,
            RoleManager<IdentityRole<Guid>> roleManager,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _organizationUnitService = organizationUnitService;
            _identityUserService = userService;
            this._roleManager = roleManager;
        }

        [HttpPost("create-organization")]
        public async Task<IdentityUserDto> CreateOrganization(OrganizationUnitCreateDto input)
        {

            var organizationUnitId = await _organizationUnitService.CreateAsync(input.OrganizationUnitDto);

            var userCreateDto = new UserCreateDto
            {
                IdentityUserDto = input.IdentityUserDto,
                RoleNames = new[] { await GetAdminRoleNames() },
                OrganizationUnitId = organizationUnitId
            };
            var user = await SetUserData(userCreateDto);
            return user;
        }

        private async Task<string> GetAdminRoleNames()
        {
            string roleName = Configuration.GetValue<string>("IdentityRole:Administrator:RoleName");
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }
            return roleName;
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