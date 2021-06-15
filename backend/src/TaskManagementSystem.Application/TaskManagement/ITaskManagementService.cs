﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementSystem.Application.TaskManagement.DTOs;

namespace TaskManagementSystem.Application.TaskManagement
{
    public interface ITaskManagementService
    {
        Task<int> CreateAndAssign(TaskCreateDto input, CancellationToken cancellationToken = default);
        Task UpdateStatus(TaskUpdateDto input);
        Task<List<TaskDto>> GetAll();
    }
}
