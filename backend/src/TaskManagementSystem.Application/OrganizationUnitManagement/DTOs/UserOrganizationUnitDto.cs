using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity.DTOs;

namespace TaskManagementSystem.Application.OrganizationUnitManagement.DTOs
{
    public class UserOrganizationUnitDto
    {
        public IdentityUserCreateDto IdentityUserDto { get; set; }
        public OrganizationUnitCreateDto OrganizationUnitDto { get; set; }
        public IdentityUserRolesDto IdentityUserRolesDto { get; set; }
    }
}
