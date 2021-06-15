using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.TaskManagement.Enums
{
    public enum TaskStatus: byte
    {
        ToDo,
        InProgress,
        Done
    }
}
