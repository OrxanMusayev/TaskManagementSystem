﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.TaskManagement.DTOs
{
    public class TaskUpdateDto
    {
        public int Id { get; set; }
        public TaskStatus Status { get; set; }
    }
}