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

        public Response()
        {
            responseBuilder = new ResponseBuilder<T>(this);
        }

        public IResponse<T> Return()
        {
            return responseBuilder.Build();
        }

        public IResponse<T> Return(T data)
        {
            return responseBuilder.WithData(data).Build();
        }

        public IResponse<T> Return(List<ValidationFailure> inputValidations = null)
        {
            return responseBuilder.WithErrors(inputValidations).Build();
        }

        //for one business error
        public IResponse<T> Return(MessageCodeEnum messageCode, string message = "")
        {
            return responseBuilder.AppendError(messageCode, message).Build();
        }

        public IResponse<T> Return(Exception ex)
        {
            return responseBuilder.WithException(ex).Build();
        }

        public IResponse<T> AppendError(ErrorModel error)
        {
            return responseBuilder
                .AppendError(error)
                .Build();
        }

        public IResponse<T> AppendError(MessageCodeEnum code, string? message = null)
        {
            return responseBuilder
                .AppendError(code, message)
                .Build();
        }

        public IResponse<T> AppendError(MessageCodeEnum code, string fieldName, string message)
        {
            return responseBuilder
                .AppendError(code, fieldName, message)
                .Build();
        }

        public IResponse<T> AppendError(ValidationFailure error)
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

        public IResponse<T> AppendErrors(List<ValidationFailure> errors)
        {
            return responseBuilder
                .AppendErrors(errors)
                .Build();
        }
    }
}
