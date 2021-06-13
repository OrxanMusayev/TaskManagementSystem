using System;

namespace TaskManagementSystem.Domain.Common.Entities.Auditing
{
    public abstract class FullAuditedEntity<TKey> : Entity<TKey>, IFullAudited
    {
        public FullAuditedEntity()
        {
        }
        public FullAuditedEntity(TKey id) : base(id)
        {

        }
        public Guid? CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? LastModificationId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
