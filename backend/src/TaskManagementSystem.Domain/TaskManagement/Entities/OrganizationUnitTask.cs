using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Common.Entities;
using TaskManagementSystem.Domain.Common.Entities.Auditing;

namespace TaskManagementSystem.Domain.TaskManagement.Entities
{
    public class OrganizationUnitTask : Entity<int>, ICreationAudited, IDeletionAudited
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public TaskStatus Status { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
