using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManagementSystem.Application.Identity;
using TaskManagementSystem.Application.OrganizationUnitManagement;

namespace TaskManagementSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IIdentityUserService, IdentityUserService>();
            services.AddScoped<IOrganizationUnitService, OrganizationUnitService>();

            return services;
        }
    }
}
