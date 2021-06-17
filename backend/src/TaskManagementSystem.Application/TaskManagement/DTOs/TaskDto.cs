using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagementSystem.Domain.TaskManagement.Enums;

namespace TaskManagementSystem.Application.TaskManagement.DTOs
{
    public class TaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public TaskStatus Status { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
    }
}
