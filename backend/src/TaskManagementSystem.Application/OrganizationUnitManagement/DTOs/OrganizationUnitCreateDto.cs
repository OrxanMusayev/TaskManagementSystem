using FluentValidation;
using FluentValidation.Validators;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Identity.DTOs;
using TaskManagementSystem.Domain.Identity;

namespace TaskManagementSystem.Application.OrganizationUnitManagement.DTOs
{
    public class OrganizationUnitCreateDto
    {
        public IdentityUserCreateDto IdentityUserDto { get; set; }
        public OrganizationUnitDto OrganizationUnitDto { get; set; }
    }

    public class UserOrganizationUnitValidator : AbstractValidator<OrganizationUnitCreateDto>
    {
        private readonly IIdentityUserManager _userManager;

        public UserOrganizationUnitValidator()
        {
        }
        public UserOrganizationUnitValidator(IIdentityUserManager userManager)
        {
            _userManager = userManager;

            RuleFor(v => v.IdentityUserDto.UserName)
                .NotEmpty().WithMessage("User name is required.")
                .MaximumLength(50).WithMessage("User name must not exceed 50 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified user name already exists.");

            RuleFor(v => v.IdentityUserDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(50).WithMessage("User name must not exceed 50 characters.")
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Email is not correct format")
                .MustAsync(BeUniqueEmail).WithMessage("This email already exists.");

            RuleFor(v => v.IdentityUserDto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password length must be greater than 6 simvols.");

            RuleFor(v => v.OrganizationUnitDto.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("This email already exists.");

            RuleFor(v => v.OrganizationUnitDto.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}").WithMessage("Phone number is not correct format")
                .MustAsync(BeUniqueTitle).WithMessage("This name already exists.");
        }

        public async Task<bool> BeUniqueTitle(string userName, CancellationToken cancellationToken)
        {
            var result = await _userManager.FindByNameAsync(userName);
            return result == null;
        }

        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var result = await _userManager.FindByEmailAsync(email);
            return result == null;
        }
    }
}
