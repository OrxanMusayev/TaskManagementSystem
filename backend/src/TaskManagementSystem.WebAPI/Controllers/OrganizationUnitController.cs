using Microsoft.AspNetCore.Mvc;
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
            IIdentityUserService userService) : base()
        {
            _organizationUnitService = organizationUnitService;
            _identityUserService = userService;
        }

        [HttpPost("organization")]
        public async Task<IdentityUserDto> CreateOrganization(UserOrganizationUnitDto input)
        {
            input.IdentityUserRolesDto.RoleNames = GetAdminRoleNames();

            var organizationUnitId = await _organizationUnitService.CreateAsync(input.OrganizationUnitDto);

            var userCreateDto = new UserCreateDto
            {
                IdentityUserDto = input.IdentityUserDto,
                IdentityUserRolesDto = input.IdentityUserRolesDto,
                OrganizationUnitId = organizationUnitId
            };
            var user = await SetUserData(userCreateDto);
            return user;
        }

        [HttpPost("organization-user")]
        public async Task<IdentityUserDto> CreateOrganizationUser(UserCreateDto input)
        {
            input.IdentityUserRolesDto.RoleNames = GetUserRoleNames();
            input.IdentityUserDto.Password = GetDefaultPassword();

            var userCreateDto = new UserCreateDto
            {
                IdentityUserDto = input.IdentityUserDto,
                IdentityUserRolesDto = input.IdentityUserRolesDto,
                OrganizationUnitId = input.OrganizationUnitId
            };
            var user = await SetUserData(userCreateDto);
            return user;
        }

        private string[] GetAdminRoleNames()
        {
            List<string> list = new() { _configuration.GetSection("IdentityRole:Admininistrator:RoleName").Value };
            List<string> roleNames = list;
            return roleNames.ToArray();
        }

        private string[] GetUserRoleNames()
        {
            List<string> roleNames = new() { _configuration.GetSection("IdentityRole:User:RoleName").Value };
            return roleNames.ToArray();
        }

        private string GetDefaultPassword() => _configuration.GetSection("IdentityRole:User:DefaultPassword").Value;

        private async Task<IdentityUserDto> SetUserData(UserCreateDto input)
        {
            var user = await _identityUserService.CreateAsync(input.IdentityUserDto);
            await _identityUserService.SetRolesAsync(user.Id, input.IdentityUserRolesDto);
            await _identityUserService.SetOrganizationUnitAsync(user.Id, input.OrganizationUnitId);
            return user;
        }
    }
}
