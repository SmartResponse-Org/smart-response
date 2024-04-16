using FluentValidation.Results;
using SmartResponse.Enums;
using SmartResponse.Interfaces;
using System;
using System.Collections.Generic;

namespace SmartResponse.Implementation
{
    public class Response<T> : IResponse<T>
    {
        public bool IsSuccess { get => responseBuilder.IsSuccess; set { } }

        public List<TErrorField> Errors { get; set; }

        public T Data { get; set; }

        private ResponseBuilder<T> responseBuilder;

        public Response()
        {
            responseBuilder = new ResponseBuilder<T>(this);
            //_messageLocalizer = LocalizerProvider<ErrorMessage>.GetLocalizer();// new CustomStringLocalizer();// (ICustomStringLocalizer) // lServiceProviderFactory.ServiceProvider.GetService(typeof(ICustomStringLocalizer));
            //_labelLocalizer =  LocalizerProvider<Label>.GetLocalizer();
            //responseBuilder = new ResponseBuilder<T>(this, _messageLocalizer, _labelLocalizer);
        }

        public IResponse<T> CreateResponse()
        {
            return responseBuilder.CreateResponse();
        }

        public IResponse<T> CreateResponse(T data)
        {
            return responseBuilder.WithData(data).Build();
        }

        public IResponse<T> CreateResponse(List<ValidationFailure> inputValidations = null)
        {
            return responseBuilder.WithErrors(inputValidations).Build();
        }

        //for one business error
        public IResponse<T> CreateResponse(MessageCodes messageCode, string message = "")
        {
            return responseBuilder.AppendError(messageCode, message).Build();
        }

        public IResponse<T> CreateResponse(Exception ex)
        {
            return responseBuilder.WithException(ex).Build();
        }

        public IResponse<T> AppendError(TErrorField error)
        {
            return responseBuilder.AppendError(error).CreateResponse();
        }

        public IResponse<T> AppendError(MessageCodes code, string? message = null)
        {
            return responseBuilder.AppendError(code, message).CreateResponse();
        }

        public IResponse<T> AppendError(MessageCodes code, string fieldName, string message)
        {
            return responseBuilder.AppendError(code, fieldName, message).CreateResponse();
        }

        public IResponse<T> AppendError(ValidationFailure error)
        {
            return responseBuilder.AppendError(error).CreateResponse();
        }

        public IResponse<T> AppendErrors(List<TErrorField> errors)
        {
            return responseBuilder.AppendErrors(errors).CreateResponse();
        }

        public IResponse<T> AppendErrors(List<ValidationFailure> errors)
        {
            return responseBuilder.AppendErrors(errors).CreateResponse();
        }
    }
}
