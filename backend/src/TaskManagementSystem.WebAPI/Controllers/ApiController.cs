using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Interfaces;

namespace TaskManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController: ControllerBase
    {
        protected readonly IConfiguration Configuration;
        protected readonly ICurrentUserService CurrentUserService;
        public ApiController(IServiceProvider services)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            Configuration = serviceProvider.GetRequiredService<IConfiguration>();
            CurrentUserService = serviceProvider.GetRequiredService<ICurrentUserService>();
        }
    }
}