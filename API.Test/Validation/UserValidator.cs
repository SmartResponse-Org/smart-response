using API.Test.Localization;
using API.Test.Models;
using FluentValidation;
using SmartResponse.Enums;
using SmartResponse.FluentValidation;

namespace API.Test.Validation
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.Username)
                .Length(10, 50).SmartResponse<UserDto, string, ErrorMessage, Label>("A-11", "10", "50");

            //RuleFor(u => u.File)
            //    .NotNull().SmartResponse(MessageCode.Required);
        }
    }
}
