using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Application.TaskManagement;
using TaskManagementSystem.Application.TaskManagement.DTOs;

namespace TaskManagementSystem.WebAPI.Controllers
{
    public class TaskController: ApiController
    {
        private readonly ITaskManagementService _taskManagementService;

        public TaskController(ITaskManagementService taskManagementService,
            IServiceProvider serviceProvider): base(serviceProvider)
        {
            _taskManagementService = taskManagementService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAndAssign(TaskCreateDto input)
        {
            var taskId = await _taskManagementService.CreateAndAssign(input);
            return taskId;
        }
    }
}
