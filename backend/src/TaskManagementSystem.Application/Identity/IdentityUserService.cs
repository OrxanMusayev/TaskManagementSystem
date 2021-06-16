﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Domain.Identity;
using TaskManagementSystem.Domain.Identity.Entities;

namespace TaskManagementSystem.Application.Identity
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly IIdentityUserManager _identityUserManager;
        private readonly IMapper _mapper;

        public IdentityUserService(IIdentityUserManager userManager,
            IMapper mapper)
        {
            _identityUserManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            var user = new ApplicationUser(
                Guid.NewGuid(),
                input.UserName,
                input.Email,
                input.Name,
                input.Surname
            );

            var result = await _identityUserManager.CreateAsync(user, input.Password);
            return _mapper.Map<IdentityUserDto>(result);
        }


        public async Task<SignInResult> SignInAsync(LoginInput input) => await _identityUserManager.SignInAsync(input.UserName, input.Password, input.RememberMe);

        public async Task<IdentityUserDto> FindByNameAsync(string userName)
        {
            var user = await _identityUserManager.FindByNameAsync(userName);
            return _mapper.Map<IdentityUserDto>(user);
        }

        public async Task<List<string>> GetUserEmails(List<Guid> userIds) => await _identityUserManager.GetEmailsById(userIds);

        public async Task<List<Claim>> GetClaimsAsync(IdentityUserDto input)
        {
            var user = _mapper.Map<ApplicationUser>(input);
            var claims = await _identityUserManager.GetClaimsAsync(user);
            return claims;
        }

        public async Task<List<string>> GetRolesAsync(IdentityUserDto input)
        {
            var user = _mapper.Map<ApplicationUser>(input);
            var roles = await _identityUserManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<IdentityResult> SetRolesAsync(Guid userId, string[] roles) => await _identityUserManager.AddToRolesAsync(userId, roles);

        public async Task SetOrganizationUnitAsync(Guid userId, Guid ouId) => await _identityUserManager.AddToOrganizationUnitAsync(userId, ouId);

        public async Task<IdentityResult> SetClaimsAsync(IdentityUserDto input, Claim[] claims)
        {
            var user = _mapper.Map<ApplicationUser>(input);
            return await _identityUserManager.AddClaimsAsync(user, claims);
        }
    }
}
