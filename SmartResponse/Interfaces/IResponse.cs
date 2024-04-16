using FluentValidation.Results;
using SmartResponse.Enums;
using SmartResponse.Implementation;
using System;
using System.Collections.Generic;

namespace SmartResponse.Interfaces
{
    public interface IResponse<T> // where T : class
    {
        public bool IsSuccess { get; set; }
        public List<TErrorField> Errors { get; set; }
        public T Data { get; set; }

        IResponse<T> CreateResponse();
        public IResponse<T> CreateResponse(T data);
        public IResponse<T> CreateResponse(List<ValidationFailure> inputValidations = null);
        //for one business error
        public IResponse<T> CreateResponse(MessageCodes messageCode, string message = "");
        public IResponse<T> CreateResponse(Exception ex);

        public IResponse<T> AppendError(TErrorField error);
        public IResponse<T> AppendError(MessageCodes code, string message);
        public IResponse<T> AppendError(MessageCodes code, string fieldName, string message);
        public IResponse<T> AppendError(ValidationFailure error);
        public IResponse<T> AppendErrors(List<TErrorField> errors);
        public IResponse<T> AppendErrors(List<ValidationFailure> errors);
    }
}