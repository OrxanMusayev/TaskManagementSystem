using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity.DTOs;

namespace TaskManagementSystem.Application.OrganizationUnitManagement.DTOs
{
    public  class UserCreateDto
    {
        public IdentityUserCreateDto IdentityUserDto { get; set; }
        public Guid OrganizationUnitId { get; set; }
        public string[] RoleNames { get; set; }
    }
}
