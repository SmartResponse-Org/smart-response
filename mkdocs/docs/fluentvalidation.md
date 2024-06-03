## Create your response model over FluentValidation.

```C#
# Use built-in SmartResponse localized error messages.
RuleFor(u => u.Username)
    .NotNull().SmartResponse(MessageCode.Required);

RuleFor(u => u.Username)
    .Length(10, 50).SmartResponse(MessageCode.InbetweenValue, "10", "50");
```