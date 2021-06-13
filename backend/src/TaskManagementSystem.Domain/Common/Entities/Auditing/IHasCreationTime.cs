using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagementSystem.Domain.Common.Entities.Auditing
{
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; set; }
    }
}
