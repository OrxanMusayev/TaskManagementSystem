using System;

namespace TaskManagementSystem.Application.Identity.DTOs
{
    public class IdentityUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
