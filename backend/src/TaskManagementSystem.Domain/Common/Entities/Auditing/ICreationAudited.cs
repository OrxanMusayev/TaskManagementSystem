using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagementSystem.Domain.Common.Entities.Auditing
{
    public interface ICreationAudited : IHasCreationTime
    {
        Guid? CreatorId { get; set; }
    }
}
