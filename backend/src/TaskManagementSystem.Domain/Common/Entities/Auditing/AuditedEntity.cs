using System;

namespace TaskManagementSystem.Domain.Common.Entities.Auditing
{
    public class AuditedEntity<TKey> : Entity<TKey>, IAudited
    {
        public AuditedEntity()
        {
        }
        public AuditedEntity(TKey id) : base(id)
        {

        }
        public Guid? CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? LastModificationId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
