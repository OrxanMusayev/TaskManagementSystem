﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.OrganizationUnitManagement.DTOs;
using TaskManagementSystem.Domain.OrganizationUnitManagement.Entities;

namespace TaskManagementSystem.Application
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrganizationUnit, OrganizationUnitCreateDto>().ReverseMap();
        }
    }
}