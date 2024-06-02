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
                .NotNull().SmartResponse(MessageCode.Required);

            RuleFor(u => u.Username)
                .Length(10, 50).SmartResponse(MessageCode.InbetweenValue, "10", "50");
        }
    }
}
