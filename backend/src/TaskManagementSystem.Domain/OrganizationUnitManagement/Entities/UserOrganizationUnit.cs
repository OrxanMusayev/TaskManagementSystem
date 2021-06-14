using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Common.Entities.Auditing;

namespace TaskManagementSystem.Domain.OrganizationUnitManagement.Entities
{
    public class UserOrganizationUnit : ICreationAudited
    {
        public Guid UserId { get; set; }
        public int OrganizationUnitId { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
