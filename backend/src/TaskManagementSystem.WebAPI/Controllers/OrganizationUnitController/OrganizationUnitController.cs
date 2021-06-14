using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Application.AccountManagement.DTOs;
using TaskManagementSystem.Application.OrganizationUnitManagement;
using TaskManagementSystem.Domain.Common.Exceptions;
using TaskManagementSystem.Domain.Identity.Entities;

namespace TaskManagementSystem.WebAPI.Controllers.OrganizationUnitController
{
    public class OrganizationUnitController: ApiController
    {
        private readonly IOrganizationUnitService _organizationUnitService;

        public OrganizationUnitController(IOrganizationUnitService organizationUnitService)
        {
            _organizationUnitService = organizationUnitService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrganizationUnitWithDefaultUser(UserOrganizationUnitDto input)
        {
            var user = await _organizationUnitService.CreateOrganizationUnitWithDefaultUser(input);

            return Ok(user);
        }
    }
}
