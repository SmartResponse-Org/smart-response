using FluentValidation.Results;
using SmartResponse.Enums;
using SmartResponse.Interfaces;
using System;
using System.Collections.Generic;

namespace SmartResponse.Implementation
{
    internal static class ResponseBuilderExtensions
    {
        internal static IResponse<T> CreateResponse<T>( this ResponseBuilder<T> response )
        {
            return response.Build();
        }

        internal static IResponse<T> CreateResponse<T>( this ResponseBuilder<T> response, T data )
        {
            return response.WithData(data).Build();
        }
      
        internal static IResponse<T> CreateResponse<T>( this ResponseBuilder<T> response, List<ValidationFailure> inputValidations = null )
        {
            return response.WithErrors(inputValidations).Build();
        }

        //for one business error
        internal static IResponse<T> CreateResponse<T>( this ResponseBuilder<T> response, MessageCodes messageCode, string message = "" )
        {
            return response.AppendError(messageCode, message).Build();
        }

        internal static IResponse<T> CreateResponse<T>( this ResponseBuilder<T> response, Exception ex )
        {
            return response.WithException(ex).Build();
        }
    }
}
