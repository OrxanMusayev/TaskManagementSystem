using AutoMapper;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Application.OrganizationUnitManagement.DTOs;
using TaskManagementSystem.Application.TaskManagement.DTOs;
using TaskManagementSystem.Domain.Identity.Entities;
using TaskManagementSystem.Domain.OrganizationUnitManagement.Entities;
using TaskManagementSystem.Domain.TaskManagement.Entities;

namespace TaskManagementSystem.Application
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrganizationUnitCreateDto, OrganizationUnit>();
            CreateMap<IdentityUserDto, ApplicationUser>();
            CreateMap<TaskCreateDto, OrganizationUnitTask>();
        }
    }
}
