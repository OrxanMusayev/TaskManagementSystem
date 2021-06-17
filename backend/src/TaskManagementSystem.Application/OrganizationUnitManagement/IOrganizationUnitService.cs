using System;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Application.OrganizationUnitManagement.DTOs;

namespace TaskManagementSystem.Application.OrganizationUnitManagement
{
    public interface IOrganizationUnitService
    {
        Task<Guid> CreateAsync(OrganizationUnitDto input);

        Task<bool> IsOrganizationExists(Guid id);
    }
}
