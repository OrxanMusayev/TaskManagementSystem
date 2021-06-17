using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Application.OrganizationUnitManagement;
using TaskManagementSystem.Application.OrganizationUnitManagement.DTOs;
using TaskManagementSystem.Domain.Common.Repositories;
using TaskManagementSystem.Domain.Identity;
using TaskManagementSystem.Domain.Identity.Entities;
using TaskManagementSystem.Domain.OrganizationUnitManagement.Entities;

namespace TaskManagementSystem.Application.Identity
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly IIdentityUserManager _identityUserManager;
        private readonly IRepository<UserOrganizationUnit, string> _userOrganizationUnitRepository;
        private readonly IRepository<OrganizationUnit, Guid> _organizationUnitRepository;
        private readonly IMapper _mapper;

        public IdentityUserService(IIdentityUserManager userManager,
            IRepository<UserOrganizationUnit, string> userOrganizationUnitRepository,
            IRepository<OrganizationUnit, Guid> organizationUnitRepository,
            IMapper mapper)
        {
            _identityUserManager = userManager;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _organizationUnitRepository = organizationUnitRepository;
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

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var user = await _identityUserManager.FindByNameAsync(userName);
            return user;
        }

        public UserDetailsDto GetUserDetails(Guid userId)
        {
            var userDetails = (from uo in _userOrganizationUnitRepository.FindBy(uo => uo.UserId == userId)
                               join o in _organizationUnitRepository.GetAll() on uo.OrganizationUnitId equals o.Id
                               join u in _identityUserManager.GetUsers() on uo.UserId equals u.Id
                               select new UserDetailsDto
                               {
                                   UserName = u.UserName,
                                   Name = u.Name,
                                   Surname = u.Surname,
                                   Email = u.Email,
                                   OrganizationName = o.Name,
                                   OrganizationAddress = o.Address,
                                   OrganizationPhoneNumber = o.PhoneNumber,
                                   OrganizationCreationTime = o.CreationTime
                               }).FirstOrDefault();

            return userDetails;
        }

        public async Task<List<string>> GetUserEmails(List<Guid> userIds) => await _identityUserManager.GetEmailsById(userIds);

        public async Task<List<Claim>> GetClaimsAsync(ApplicationUser input)
        {
            var user = _mapper.Map<ApplicationUser>(input);
            var claims = await _identityUserManager.GetClaimsAsync(user);
            return claims;
        }

        public async Task<List<string>> GetRolesAsync(ApplicationUser input)
        {
            var user = _mapper.Map<ApplicationUser>(input);
            var roles = await _identityUserManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<IdentityResult> SetRolesAsync(Guid userId, string[] roles) => await _identityUserManager.AddToRolesAsync(userId, roles);

        public async Task SetOrganizationUnitAsync(Guid userId, Guid ouId) => await _identityUserManager.AddToOrganizationUnitAsync(userId, ouId);

        public async Task<IdentityResult> SetClaimsAsync(ApplicationUser input, Claim[] claims)
        {
            var user = _mapper.Map<ApplicationUser>(input);
            return await _identityUserManager.AddClaimsAsync(user, claims);
        }
    }
}
