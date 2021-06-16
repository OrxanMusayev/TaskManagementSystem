using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.TaskManagement.DTOs
{
    public class TaskCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public List<Guid> UserIds { get; set; }
    }

    public class TaskCreateDtoValidation : AbstractValidator<TaskCreateDto>
    {
        public TaskCreateDtoValidation()
        {
            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Description is required.");
        }
    }
}
