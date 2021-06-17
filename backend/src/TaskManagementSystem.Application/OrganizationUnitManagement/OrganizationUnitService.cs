using AutoMapper;
using System;
using System.Threading.Tasks;
using TaskManagementSystem.Application.OrganizationUnitManagement.DTOs;
using TaskManagementSystem.Domain.Common.Repositories;
using TaskManagementSystem.Domain.OrganizationUnitManagement.Entities;

namespace TaskManagementSystem.Application.OrganizationUnitManagement
{
    public class OrganizationUnitService : IOrganizationUnitService
    {
        private readonly IRepository<OrganizationUnit, Guid> _organizationUnitRepository;
        private readonly IMapper _mapper;

        public OrganizationUnitService(IRepository<OrganizationUnit, Guid> organizationUnitRepository,
                                       IMapper mapper)
        {
            _organizationUnitRepository = organizationUnitRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(OrganizationUnitDto input)
        {
            var organizationUnit = _mapper.Map<OrganizationUnit>(input);
            await _organizationUnitRepository.Add(organizationUnit);
            return organizationUnit.Id;
        }
    }
}