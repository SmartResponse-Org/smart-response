using FluentValidation.Results;
using SmartResponse.Enums;
using SmartResponse.Models;
using System;
using System.Collections.Generic;

namespace SmartResponse.Interfaces
{
    public interface IResponseBuilder<T> // where T : class
    {
        IResponse<T> Set(MessageCode code, string? fieldName = null, params string[] labels);

        IResponse<T> Set<Error, Label>(string code, string? fieldName = null, params string[] labels);

        IResponse<T> Set(List<ValidationFailure> errors);

        IResponse<T> Set(ErrorModel error);

        IResponse<T> Set(List<ErrorModel> errors);
        
        IResponse<T> Set(Exception ex);

        IResponseBuilder<T> Append(MessageCode code, string? fieldName = null, params string[] labels);

        IResponseBuilder<T> Append<Error, Label>(string code, string? fieldName = null, params string[] labels);

        IResponseBuilder<T> Append(List<ValidationFailure> errors);

        IResponseBuilder<T> Append(ErrorModel error);

        IResponseBuilder<T> Append(List<ErrorModel> errors);

        IResponseBuilder<T> Append(Exception ex);

        IResponse<T> Build(T data);

        IResponse<T> Build();
    }
}