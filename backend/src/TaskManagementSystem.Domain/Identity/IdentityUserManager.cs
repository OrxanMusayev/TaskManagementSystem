using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Common.Exceptions;
using TaskManagementSystem.Domain.Identity.Entities;

namespace TaskManagementSystem.Domain.Identity
{
    public class IdentityUserManager: IIdentityUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityUserManager(UserManager<ApplicationUser> userManager,
                                   SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //Create User

        public async Task<ApplicationUser> Register(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return user;
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

    }
}
