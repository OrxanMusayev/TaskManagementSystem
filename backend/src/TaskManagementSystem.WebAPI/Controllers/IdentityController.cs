using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Application.OrganizationUnitManagement;
using TaskManagementSystem.Application.OrganizationUnitManagement.DTOs;
using TaskManagementSystem.Domain.Common.Exceptions;

namespace TaskManagementSystem.WebAPI.Controllers
{
    [Authorize]
    public class IdentityController : ApiController
    {
        private readonly IIdentityUserService _identityUserService;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IOrganizationUnitService _organizationUnitService;

        public IdentityController(IIdentityUserService identityUserService,
                                  RoleManager<IdentityRole<Guid>> roleManager,
                                  IOrganizationUnitService organizationUnitService,
                                  IServiceProvider serviceProvider): base(serviceProvider)
        {
            _identityUserService = identityUserService;
            this._roleManager = roleManager;
            this._organizationUnitService = organizationUnitService;
        }

        [HttpPost("create-user")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IdentityUserDto>> CreateOrganizationUser(OrganizationUnitUserCreateDto input)
        {
            input.IdentityUserDto.Password = GetDefaultPassword();

            if (await _organizationUnitService.IsOrganizationExists(input.OrganizationUnitId))
            {
                var userCreateDto = new UserCreateDto
                {
                    IdentityUserDto = input.IdentityUserDto,
                    RoleNames = new[] { await GetUserRoleNames() },
                    OrganizationUnitId = input.OrganizationUnitId
                };
                var user = await SetUserData(userCreateDto);
                return Ok(user);
            }
            else {
                return NotFound("There is no ogranization with given id");
            }
            
        }

        [HttpGet("user-details")]
        public UserDetailsDto GetUserDetails()
        {
            var userId = (Guid) CurrentUserService.UserId;
            return _identityUserService.GetUserDetails(userId);
        }

        private string GetDefaultPassword() => Configuration.GetValue<string>("IdentityRole:User:DefaultPassword");

        private async Task<string> GetUserRoleNames()
        {
            string roleName = Configuration.GetValue<string>("IdentityRole:User:RoleName");
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
