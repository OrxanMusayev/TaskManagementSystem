using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public async Task<SignInResult> SignInAsync(string userName, string password, bool rememberMe) => await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);

        public IQueryable<ApplicationUser> GetUsers() =>  _userManager.Users;

        public IQueryable<ApplicationUser> GetById(Guid userId) => _userManager.Users.Where(u => u.Id == userId);
        public async Task<List<string>> GetEmailsById(List<Guid> ids) => _userManager.Users.Where(u => ids.Contains(u.Id)).Select(u => u.Email).ToList();

        public async Task<List<Claim>> GetClaimsAsync(ApplicationUser user) => new List<Claim>(await _userManager.GetClaimsAsync(user));

        public async Task<List<string>> GetRolesAsync(ApplicationUser user) => new List<string>(await _userManager.GetRolesAsync(user));

        public async Task<ApplicationUser> FindByNameAsync(string userName) => await _userManager.FindByNameAsync(userName);

        public async Task<ApplicationUser> FindByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);

        public async Task<ApplicationUser> FindByIdAsync(Guid id) => await _userManager.FindByIdAsync(id.ToString());

        public async Task<IdentityResult> AddToRolesAsync(Guid id, string[] roles)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user==null)
            {
                throw new EntityNotFoundException(typeof(ApplicationUser), id);
            }

            var result = await _userManager.AddToRolesAsync(user, roles);
            await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult> AddClaimsAsync(ApplicationUser user, Claim[] claims) => await _userManager.AddClaimsAsync(user, claims);

        public async Task AddToOrganizationUnitAsync(Guid userId, Guid ouId)
        {
            await AddToOrganizationUnitAsync(
                await _userManager.FindByIdAsync(userId.ToString()),
                await _organizationUnitRepository.GetFirst(ouId)
            );
        }

        public async Task AddToOrganizationUnitAsync(ApplicationUser user, OrganizationUnit ou)
        {
            if (await _userOrganizationUnit.Any(uou => uou.OrganizationUnitId == ou.Id && uou.UserId == user.Id))
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
