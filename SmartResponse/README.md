## Smart Response 

A .NET library for building custom API response. 

## Usage

```C#
using SmartResponse;

# Create response model.
var response = ResponseManager<bool>.Create();

# return error.
return response.Set(MessageCode.Required, "Name");

# append errors.
response.Append(MessageCode.InbetweenValue, "Age", "18", "25");

response.Append(MessageCode.InvalidMinLength, "Name", "Name", "10");

return response.Build();

# return error using fluent validation.
RuleFor(u => u.Username)
    .NotNull().SmartResponse(MessageCode.Required);

RuleFor(u => u.Username)
    .Length(10, 50).SmartResponse(MessageCode.InbetweenValue, "10", "50");
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.