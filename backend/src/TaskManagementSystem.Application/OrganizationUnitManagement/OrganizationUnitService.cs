using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TaskManagementSystem.Application.AccountManagement.DTOs;
using TaskManagementSystem.Application.Identity;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Domain.Common.Exceptions;
using TaskManagementSystem.Domain.Common.Repositories;
using TaskManagementSystem.Domain.Identity.Entities;
using TaskManagementSystem.Domain.OrganizationUnitManagement.Entities;

namespace TaskManagementSystem.Application.OrganizationUnitManagement
{
    public class OrganizationUnitService: IOrganizationUnitService
    {
        private readonly IRepository<OrganizationUnit, int> _organizationUnitRepository;
        private readonly IIdentityUserService _userService;
        private readonly IMapper _mapper;

        public OrganizationUnitService(IRepository<OrganizationUnit, int> organizationUnitRepository,
            IIdentityUserService userService,
            IMapper mapper)
        {
            _organizationUnitRepository = organizationUnitRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ApplicationUser> CreateOrganizationUnitWithDefaultUser(UserOrganizationUnitDto input)
        {
            var organizationUnit = _mapper.Map<OrganizationUnit>(input.OrganizationUnitDto);
            await _organizationUnitRepository.Add(organizationUnit);

            var result = await _userService.CreateAsync(input.IdentityUserDto);
            return result;
        }
    }
}
