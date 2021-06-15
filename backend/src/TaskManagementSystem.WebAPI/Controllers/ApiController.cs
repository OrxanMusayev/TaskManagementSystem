using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController: ControllerBase
    {
        protected readonly IConfiguration _configuration;

        public ApiController()
        {
        }
        public ApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
