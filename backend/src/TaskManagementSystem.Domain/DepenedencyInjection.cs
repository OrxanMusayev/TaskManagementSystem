using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Identity;

namespace TaskManagementSystem.Domain
{
    public static class DepenedencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IIdentityUserManager, IdentityUserManager>();
            return services;
        }
    }
}
