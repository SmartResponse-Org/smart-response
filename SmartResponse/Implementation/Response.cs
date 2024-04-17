using FluentValidation.Results;
using SmartResponse.Enums;
using SmartResponse.Interfaces;
using SmartResponse.Models;
using System;
using System.Collections.Generic;

namespace SmartResponse.Implementation
{
    public class Response<T> : IResponse<T>
    {
        public bool IsSuccess { get => responseBuilder.IsSuccess; set { } }

        public List<ErrorModel> Errors { get; set; }

        public T Data { get; set; }

        private ResponseBuilder<T> responseBuilder;

        public Response(Culture culture)
        {
            responseBuilder = new ResponseBuilder<T>(this, culture);
        }


        public IResponse<T> Finish()
        {
            return responseBuilder.Build();
        }

        public IResponse<T> Finish(T data)
        {
            return responseBuilder.WithData(data).Build();
        }


        public IResponse<T> Finish(List<ValidationFailure> inputValidations = null)
        {
            return responseBuilder.WithErrors(inputValidations).Build();
        }


        //for one business error
        public IResponse<T> Finish(MessageCode code, string? fieldName, params string[] labels)
        {
            return responseBuilder
                .AppendError(code, fieldName, labels)
                .Build();
        }

        public IResponse<T> Finish<Error, Label>(string code, string? fieldName, params string[] labels)
        {
            return responseBuilder
                .AppendError<Error, Label>(code, fieldName, labels)
                .Build();
        }

        public IResponse<T> Finish(Exception ex)
        {
            return responseBuilder.WithException(ex).Build();
        }


        public IResponse<T> AppendError(ErrorModel error)
        {
            return responseBuilder
                .AppendError(error)
                .Build();
        }

        public IResponse<T> AppendErrors(List<ErrorModel> errors)
        {
            return responseBuilder
                .AppendErrors(errors)
                .Build();
        }


        public IResponse<T> AppendError(MessageCode code, string? fieldName, params string[] labels)
        {
            return responseBuilder
                .AppendError(code, fieldName, labels)
                .Build();
        }

        public IResponse<T> AppendError<Error, Label>(string code, string? fieldName, params string[] labels)
        {
            return responseBuilder
                .AppendError<Error, Label>(code, fieldName, labels)
                .Build();
        }

        
        public IResponse<T> AppendError(ValidationFailure error)
        {
            return responseBuilder
                .AppendError(error)
                .Build();
        }

        public IResponse<T> AppendErrors(List<ValidationFailure> errors)
        {
            return responseBuilder
                .AppendErrors(errors)
                .Build();
        }
    }
}
