using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.TaskManagement.DTOs
{
    public class TaskListResultDto
    {
        public TaskListResultDto(long totalCount, List<TaskDto> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
        public List<TaskDto> Items { get; set; }
        public long TotalCount { get; set; }
    }
}
