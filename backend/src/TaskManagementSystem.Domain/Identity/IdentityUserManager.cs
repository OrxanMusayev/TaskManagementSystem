using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Common.Exceptions;
using TaskManagementSystem.Domain.Common.Repositories;
using TaskManagementSystem.Domain.Identity.Entities;
using TaskManagementSystem.Domain.OrganizationUnitManagement.Entities;

namespace TaskManagementSystem.Domain.Identity
{
    public class IdentityUserManager: IIdentityUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRepository<OrganizationUnit, Guid> _organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, string> _userOrganizationUnit;

        public IdentityUserManager(UserManager<ApplicationUser> userManager,
                                   SignInManager<ApplicationUser> signInManager,
                                   IRepository<OrganizationUnit, Guid> organizationUnitRepository,
                                   IRepository<UserOrganizationUnit, string> userOrganizationUnit)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnit = userOrganizationUnit;
        }

        public async Task<ApplicationUser> CreateAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return user;
        }

        public async Task<List<string>> GetUsersEmailsById(List<Guid> ids)
        {
            var emailAddresses = _userManager.Users.Where(u => ids.Contains(u.Id)).Select(u => u.Email).ToList();
            return emailAddresses;
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<ApplicationUser> FindByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user;
        }

        public async Task<IdentityResult> AddToRolesAsync(Guid id, string[] roles)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user==null)
            {
                throw new EntityNotFoundException(typeof(ApplicationUser), id);
            }

            await _userManager.AddToRolesAsync(user, roles);

            return IdentityResult.Success;
        }

        public async Task AddToOrganizationUnitAsync(Guid userId, Guid ouId)
        {
            await AddToOrganizationUnitAsync(
                await _userManager.FindByIdAsync(userId.ToString()),
                await _organizationUnitRepository.GetFirst(ouId)
            );
        }

        public async Task AddToOrganizationUnitAsync(ApplicationUser user, OrganizationUnit ou)
        {
            if (user.OrganizationUnits.Any(uou => uou.OrganizationUnitId == ou.Id))
            {
                return;
            }

            var userOrganizationiUnit = new UserOrganizationUnit
            {
                UserId = user.Id,
                OrganizationUnitId = ou.Id
            };

            await _userOrganizationUnit.Add(userOrganizationiUnit);
            await _userManager.UpdateAsync(user);
        }
    }
}
