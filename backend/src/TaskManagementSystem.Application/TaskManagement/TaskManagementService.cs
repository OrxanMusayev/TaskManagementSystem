using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementSystem.Application.TaskManagement.DTOs;
using TaskManagementSystem.Domain.Common.Exceptions;
using TaskManagementSystem.Domain.Common.Repositories;
using TaskManagementSystem.Domain.Emailing;
using TaskManagementSystem.Domain.Identity;
using TaskManagementSystem.Domain.OrganizationUnitManagement.Entities;
using TaskManagementSystem.Domain.TaskManagement.Entities;

namespace TaskManagementSystem.Application.TaskManagement
{
    public class TaskManagementService: ITaskManagementService
    {
        private readonly IRepository<OrganizationUnitTask, int> _taskRepository;
        private readonly IRepository<TaskUsers, int> _taskUsersRepository;
        private readonly IRepository<UserOrganizationUnit, string> _userOrganizationUnitRepository;
        private readonly IEmailSender _emailSender;
        private readonly IIdentityUserManager _identityUserManager;
        private readonly IMapper _mapper;

        public TaskManagementService(IRepository<OrganizationUnitTask, int> taskRepository,
                                     IRepository<TaskUsers, int> taskUsersRepository,
                                     IRepository<UserOrganizationUnit, string> userOrganizationUnitRepository,
                                     IEmailSender emailSender,
                                     IIdentityUserManager identityUserManager,
                                     IMapper mapper
                                     )
        {
            _taskRepository = taskRepository;
            _taskUsersRepository = taskUsersRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _emailSender = emailSender;
            _identityUserManager = identityUserManager;
            _mapper = mapper;
        }

        public async Task<int> CreateAndAssign(TaskCreateDto input, CancellationToken cancellationToken = default)
        {
            var task = _mapper.Map<OrganizationUnitTask>(input);
            await _taskRepository.Add(task);
            await _taskRepository.Commit(cancellationToken);

            await AssignTaskToUsers(input, task.Id, cancellationToken);
            
            return task.Id;
        }

        public async Task UpdateStatus(TaskUpdateDto input)
        {
            var task = _taskRepository.FindBy(t => t.Id == input.Id).FirstOrDefault();
            task.Status = input.Status;
            await _taskRepository.Update(task);
            await _taskRepository.Commit();
        }

        public TaskListResultDto GetListByUserId(Guid userId)
        {
            var organizationId = _userOrganizationUnitRepository.FindBy(uo => uo.UserId == userId).Select(x => x.OrganizationUnitId).FirstOrDefault();
            var taskQuery = from t in _taskRepository.GetAll()
                            join tu in _taskUsersRepository.GetAll() on t.Id equals tu.TaskId
                            join uo in _userOrganizationUnitRepository.FindBy(uo => uo.OrganizationUnitId == organizationId) on tu.UserId equals uo.UserId
                            join u in _identityUserManager.GetUsers() on uo.UserId equals u.Id
                            select new TaskDto
                            {
                                Title = t.Title,
                                Description = t.Description,
                                Deadline = t.Deadline,
                                Status = t.Status,
                                UserName = u.Name,
                                UserSurname = u.Surname
                            };
                            

            var taskDto = taskQuery.ToList();
            var totalCount = taskQuery.Count();

            return new TaskListResultDto(totalCount, taskDto);
        }

        private async Task AssignTaskToUsers(TaskCreateDto input, int taskId, CancellationToken cancellationToken)
        {
            input.UserIds.ForEach(async userId =>
            {
                var taskUsers = new TaskUsers
                {
                    TaskId = taskId,
                    UserId = userId
                };
                await _taskUsersRepository.Add(taskUsers);
            });
            await _taskUsersRepository.Commit(cancellationToken);
            await NotifyUsers(input.UserIds, input.Title + "-" + taskId, input.Description);
        }

        private async Task NotifyUsers(List<Guid> userIds, string taskTitle, string taskDescription)
        {
            List<string> emailAddresses = await _identityUserManager.GetEmailsById(userIds);

            try
            {
                await _emailSender.SendAsync(emailAddresses, $"Assigned {taskTitle} to you", taskDescription);
            }
            catch
            {
                throw new UserFriendlyException("Email could not send!");
            }
        }
    }
}
