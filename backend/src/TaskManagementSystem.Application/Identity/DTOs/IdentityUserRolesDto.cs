using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Identity;

namespace TaskManagementSystem.Application.Identity.DTOs
{
    public class IdentityUserRolesDto
    {
        public string[] RoleNames { get; set; }
    }
}
