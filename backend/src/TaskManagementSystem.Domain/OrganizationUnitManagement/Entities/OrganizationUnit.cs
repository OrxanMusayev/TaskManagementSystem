using System;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementSystem.Domain.Common.Entities.Auditing;

namespace TaskManagementSystem.Domain.OrganizationUnitManagement.Entities
{
    public class OrganizationUnit: FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
