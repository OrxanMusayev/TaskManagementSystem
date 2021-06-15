using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Common.Entities;
using TaskManagementSystem.Domain.Common.Entities.Auditing;

namespace TaskManagementSystem.Domain.OrganizationUnitManagement.Entities
{
    public class UserOrganizationUnit : Entity<string>, ICreationAudited
    {
        [NotMapped]
        public override string Id { get; set; }
        public Guid UserId { get; set; }
        public Guid OrganizationUnitId { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}