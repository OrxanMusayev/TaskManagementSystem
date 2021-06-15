using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Identity.Entities;

namespace TaskManagementSystem.Domain.Identity
{
    public interface IIdentityUserManager
    {
        Task<ApplicationUser> Register(ApplicationUser input, string password);
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<ApplicationUser> FindByEmailAsync(string email);
    }
}
