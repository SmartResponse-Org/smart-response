using FluentValidation.Results;
using SmartResponse.Enums;
using SmartResponse.Models;
using System;
using System.Collections.Generic;

namespace SmartResponse.Interfaces
{
    public interface IResponse<T> // where T : class
    {
        bool IsSuccess { get; set; }
        List<ErrorModel> Errors { get; set; }
        T Data { get; set; }

        IResponse<T> Return();
        IResponse<T> Return(T data);
        IResponse<T> Return(List<ValidationFailure> inputValidations = null);
        //for one business error
        IResponse<T> Return(MessageCodeEnum messageCode, string message = "");
        IResponse<T> Return(Exception ex);

        IResponse<T> AppendError(ErrorModel error);
        IResponse<T> AppendError(MessageCodeEnum code, string message);
        IResponse<T> AppendError(MessageCodeEnum code, string fieldName, string message);
        IResponse<T> AppendError(ValidationFailure error);
        IResponse<T> AppendErrors(List<ErrorModel> errors);
        IResponse<T> AppendErrors(List<ValidationFailure> errors);
    }
}