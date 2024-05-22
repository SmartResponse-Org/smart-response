using FluentValidation.Results;
using SmartResponse.Enums;
using SmartResponse.Models;
using System;
using System.Collections.Generic;

namespace SmartResponse.Interfaces
{
    public interface IResponseBuilder<T> // where T : class
    {
        /// <summary>
        /// Set error using built-in error codes and labels.
        /// </summary>
        IResponse<T> Set(MessageCode code, string? fieldName = null, params string[] labels);

        /// <summary>
        /// Set error using built-in error codes and custom labels.
        /// </summary>
        IResponse<T> Set<Label>(MessageCode code, string? fieldName, params string[] labels);

        /// <summary>
        /// Set error using custom error codes and built-in labels.
        /// </summary>
        IResponse<T> Set<Error>(string code, string? fieldName, params string[] labels);

        /// <summary>
        /// Set error using custom error codes without labels.
        /// </summary>
        IResponse<T> Set<Error>(string code, string? fieldName);

        /// <summary>
        /// Set error using custom error codes and custom labels.
        /// </summary>
        IResponse<T> Set<Error, Label>(string code, string? fieldName = null, params string[] labels);

        IResponse<T> Set(List<ValidationFailure> errors);

        IResponse<T> Set(ErrorModel error);

        IResponse<T> Set(List<ErrorModel> errors);
        
        IResponse<T> Set(Exception ex);

        /// <summary>
        /// Append error using built-in error codes and labels.
        /// </summary>
        IResponseBuilder<T> Append(MessageCode code, string? fieldName = null, params string[] labels);

        /// <summary>
        /// Append error using built-in error codes and custom labels.
        /// </summary>
        IResponseBuilder<T> Append<Label>(MessageCode code, string? fieldName, params string[] labels);

        /// <summary>
        /// Append error using custom error codes and built-in labels.
        /// </summary>
        IResponseBuilder<T> Append<Error>(string code, string? fieldName, params string[] labels);

        /// <summary>
        /// Append error using custom error codes without labels.
        /// </summary>
        IResponseBuilder<T> Append<Error>(string code, string? fieldName);

        /// <summary>
        /// Append error using custom error codes and custom labels.
        /// </summary>
        IResponseBuilder<T> Append<Error, Label>(string code, string? fieldName = null, params string[] labels);

        IResponseBuilder<T> Append(List<ValidationFailure> errors);

        IResponseBuilder<T> Append(ErrorModel error);

        IResponseBuilder<T> Append(List<ErrorModel> errors);

        IResponseBuilder<T> Append(Exception ex);

        IResponse<T> Build(T data);

        IResponse<T> Build();
    }
}