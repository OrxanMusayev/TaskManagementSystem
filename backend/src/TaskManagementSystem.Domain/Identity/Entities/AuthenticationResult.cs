using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Identity.Entities
{
    public class AuthenticationResult
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public string Error { get; set; }
    }
}
