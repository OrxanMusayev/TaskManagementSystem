using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Application.TaskManagement;
using TaskManagementSystem.Application.TaskManagement.DTOs;

namespace TaskManagementSystem.WebAPI.Controllers
{
    [Authorize]
    public class TaskController: ApiController
    {
        private readonly ITaskManagementService _taskManagementService;

        public TaskController(ITaskManagementService taskManagementService,
                              IServiceProvider serviceProvider): base(serviceProvider)
        {
            _taskManagementService = taskManagementService;
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> CreateAndAssign(TaskCreateDto input)
        {
            await _taskManagementService.CreateAndAssign(input);
            return NoContent();
        }

        [HttpGet]
        public TaskListResultDto GetTasks()
        {
            var userId = (Guid)CurrentUserService.UserId;   
            var taskList = _taskManagementService.GetListByUserId(userId);
            return taskList;
        }
    }
}
