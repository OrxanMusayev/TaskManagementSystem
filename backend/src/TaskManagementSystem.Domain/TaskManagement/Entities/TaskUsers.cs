using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Common.Entities;
using TaskManagementSystem.Domain.Common.Entities.Auditing;

namespace TaskManagementSystem.Domain.TaskManagement.Entities
{
    public class TaskUsers : Entity<int>, ICreationAudited
    {
        public int TaskId { get; set; }
        public Guid UserId { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
