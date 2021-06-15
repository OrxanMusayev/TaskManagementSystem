using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Mappings;
using TaskManagementSystem.Domain.OrganizationUnitManagement.Entities;

namespace TaskManagementSystem.Application.OrganizationUnitManagement.DTOs
{
    public class OrganizationUnitCreateDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
