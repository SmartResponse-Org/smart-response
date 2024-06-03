
![logo](https://lh3.googleusercontent.com/fife/ALs6j_ESKuE_1ozfwl1S_Nh8tmUw4yg53Pe4XsREQclQvsvnwkfdVVw6VT__MHElNEApqBxldXp7Q3ngoPbvm-5GbkV9zfYaOqG2If20tPBJsJJQPjtI7aYpUVsoV4tgmfX57jdLsXqPxd-TmgcEzCKvQzSjXs1SPMU-TNEB1A6xsdStuf2PJjdLYggxV9VU_7RggPXPHzrJmayyaPR6-euJTDjMVu-ClxHnVQIZ6y1IK-P8aSjfPvAFnvhO1CtwsK87694ClBSLzjwMdsykyRjriwQZwEZBtdFP1U3-YXDxiG9G7xaoHQjfcQSFYw5YR2t1xSKJ4fJEzHLzH6Dw_wdrF1_eXzuV-G5IsvPmErup6arZcf3Ua4oU1vI1tjXWBDx0O-Ikpv2J8q3ZMdkCWs6izmBA35DyA6K7v3shSHz8o9T297FOIMzHKIu4P5vjhQtqtcfpm4RyGAcQPCqcAykK1Wt4OqnQBOwEgwNLDl5urA7RqkSGYc3PPElkoeDht2zVoYT3wWYaKY0q9qFxH584n8FvM97dZsf9IlgxgAxl6Q9f_SEa8KGQIgUSe12uGaWP5qZsDt5Wa1ajYX8DRXoyn-eiXF6zLLQnNHw6Y6KIBsoJrqZE3VziuIsBTJTeDbgOO6VRHGyyIL6zpTNui0h1SDt9IFRg63C4R9t8QeT7JUXCBJGgQHHuxwAZkzLTzOU36dJWS--ad-qYIyrSI0c2sQZQMkqdc9xan4WkyjfVBD3pigzSXh6JrkSRRGohiQpsh73h2eyZEqB0Xcl6zAwS2z9l9AAsALq01lFhkA9bW0eiuapmoldnLnt3-GBxILUCV460Q2SDSBTHHdNzxDGOmZeFgjtP6gaWyThH5M-NhrKxg6XzlyrAZzrWGwJ4pBzaIFyyKNthJ0cFvmcmGdoOkL-2SHzBS_46rPvX7k6-zdbglIO_O6lEkA7fjYv6x2CRBMBnMGapb5u3ym66HYcjGLvcDRaNp82Ah682_Q3oxtFliU6_xdLh0KxIRVeer105H3pkpe8a1lWraU_g8rsK3CzR7Oc7cpQVGGVay2ShGobz9YKmfp-cuWsJc-F3ibvA_HIzUSiGNKWt4gdAXH2AHXOf4dZeb-HcqSstyB5QxrsIaCMwur79-vtf9Zw-PN3kTj_zv4nn7VSFebjCewZJRzbbrh9YyWalZMWciZGbrhJ6ZTifCSwQLMGKcdFvWWQw6BGf_z50hRrHj5JqJ4X1nFLA0Ge_P6j0D3sWUd2bYbxWcHj71n-qmnRtY6yBlfmkWcRJlZNXr1X3Ba_Y36n4okwRZL_JEDhiOFrieZ7rO8LscXzwnIsosDHap_mhVidqrWYweQseHia3720EXGLT8OkAyQAZ09ra3pX2Vuifi95_3uc12__DccpLdD6pU7GKTsraxVULPuK33W_C971zWTrl5f3BoeKasKgAKG1MBRwAQISztZLdL250PtIGQQnw-3UgS3E-bo2oo39iNSisw596RNfmrUl0h6iNMNydFYI8KYohrmNY7DSddxa7-ey5REB16Cg8Q1EXGpec2pEMNY_-VvmlNhLDXTZeOSJXvfl32hZmZg1X5Ctv4m3lAtwno-ZST7YsokrIvI5Vtx0GWQ=w1920-h878)

# 

A .NET library for building custom API response. 

## Installation

Use the package manager [Nuget](https://www.nuget.org/packages/SmartResponse) to install Smart Response.

```PMC
NuGet\Install-Package SmartResponse
```

## Usage

```C#
using SmartResponse;

# register Smart Response in services.
builder.Services.AddSmartResponse();

# create response model.
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