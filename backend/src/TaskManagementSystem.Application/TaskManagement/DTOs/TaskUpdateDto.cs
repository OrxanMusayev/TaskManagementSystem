﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagementSystem.Domain.TaskManagement.Enums;

namespace TaskManagementSystem.Application.TaskManagement.DTOs
{
    public class TaskUpdateDto
    {
        public int Id { get; set; }
        public TaskStatus Status { get; set; }
    }
}
