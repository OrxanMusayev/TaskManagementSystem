using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TaskManagementSystem.Domain.Common.Entities.Auditing;
using TaskManagementSystem.Domain.OrganizationUnitManagement.Entities;

namespace TaskManagementSystem.Domain.Identity.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IFullAudited
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? LastModificationId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        protected ApplicationUser()
        {
        }

        public ApplicationUser(
            Guid id,
            [NotNull] string userName,
            [NotNull] string email,
            [AllowNull] string name = null,
            [AllowNull] string surname = null)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Name = name;
            Surname = surname;
        }

        public virtual ICollection<UserOrganizationUnit> OrganizationUnits { get; set; }

    }
}
