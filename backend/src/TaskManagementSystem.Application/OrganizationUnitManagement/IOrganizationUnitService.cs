using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.AccountManagement.DTOs;
using TaskManagementSystem.Domain.Identity.Entities;

namespace TaskManagementSystem.Application.OrganizationUnitManagement
{
    public interface IOrganizationUnitService
    {
        Task<ApplicationUser> CreateOrganizationUnitWithDefaultUser(UserOrganizationUnitDto input);
    }
}
