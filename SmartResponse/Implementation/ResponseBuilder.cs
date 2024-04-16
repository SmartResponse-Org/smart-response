using FluentValidation.Results;
using SmartResponse.Enums;
using SmartResponse.Interfaces;
using SmartResponse.Localization;
using SmartResponse.Models;
using System;
using System.Collections.Generic;

namespace SmartResponse.Implementation
{
    public class ResponseBuilder<T>
    {
        private bool _isSuccess;
        private T _data;
        private List<ErrorModel> _errors;
        private Exception _exception;
        private IResponse<T> _response;
        private ICustomStringLocalizer<ErrorMessage> _messageLocalizer;
        private ICustomStringLocalizer<Label> _labelLocalizer;

        public ResponseBuilder(IResponse<T> response)
        {
            //this.data = Activator.CreateInstance<T>();//TODO:recheck this
            _response = response;
            _errors = new List<ErrorModel>();
            _exception = null;
            _messageLocalizer = LocalizerProvider<ErrorMessage>.GetLocalizer();
            _labelLocalizer = LocalizerProvider<Label>.GetLocalizer();
        }

        public ResponseBuilder<T> AppendError(ErrorModel error)
        {
            _errors.Add(error);

            return this;
        }

        public ResponseBuilder<T> AppendError(MessageCodeEnum code, string message)
        {
            _errors.Add(new ErrorModel
            {
                FieldName = "",
                Code = code.StringValue(),
                Message = !string.IsNullOrWhiteSpace(message)
                                ? _messageLocalizer[code.StringValue(), _labelLocalizer[message]]
                                : _messageLocalizer[code.StringValue()]
            });

            return this;
        }

        public ResponseBuilder<T> AppendError(MessageCodeEnum code, string fieldName, string message)
        {
            _errors.Add(new ErrorModel
            {
                FieldName = fieldName,
                Code = code.StringValue(),
                Message = _messageLocalizer[code.StringValue(), !string.IsNullOrWhiteSpace(message) ? $"[" + _labelLocalizer[message] + "]" : message]
            });

            return this;
        }

        public ResponseBuilder<T> AppendError(ValidationFailure error)
        {
            return AppendErrors(new List<ValidationFailure> { error });
        }

        public ResponseBuilder<T> AppendErrors(List<ErrorModel> errors)
        {
            _errors.AddRange(errors);

            return this;
        }

        public ResponseBuilder<T> AppendErrors(List<ValidationFailure> errors)
        {
            foreach (var item in errors)
            {
                _errors.Add(new ErrorModel

                {
                    FieldName = item.PropertyName,
                    Code = item.ErrorCode,
                    Message = item.ErrorMessage,
                    FieldLang = item.AttemptedValue?.ToString()
                });
            }

            return this;
        }

        public ResponseBuilder<T> WithError(ErrorModel error)
        {
            return WithErrors(new List<ErrorModel> { error });
        }

        public ResponseBuilder<T> WithError(ValidationFailure error)
        {
            return WithErrors(new List<ValidationFailure> { error });
        }

        public ResponseBuilder<T> WithErrors(List<ErrorModel> errors)
        {
            _errors.AddRange(errors);
            
            return this;
        }

        public ResponseBuilder<T> WithErrors(List<ValidationFailure> errors)
        {
            foreach (var item in errors)
            {
                item.PropertyName = item.PropertyName == "File.File" ? "File" : item.PropertyName;

                string localizedFieldName = _labelLocalizer[item.PropertyName];

                _errors.Add(new ErrorModel
                {
                    FieldName = item.PropertyName,
                    //FieldName = !string.IsNullOrWhiteSpace(localizedFieldName)
                    //          ? $"[" + localizedFieldName + "]"
                    //          : $"[" + item.PropertyName + "]",
                    Code = item.ErrorCode,
                    Message = string.Format(item.ErrorMessage, !string.IsNullOrWhiteSpace(localizedFieldName)
                            ? $"[" + _labelLocalizer[item.PropertyName] + "]"
                            : $"[" + item.PropertyName + "]"),
                    //for (Default,Ar) in Required Fields with  jsonmodel values
                    FieldLang = item.ErrorCode == MessageCodeEnum.Required.StringValue() ? item.AttemptedValue?.ToString() : null
                });
            }

            return this;
        }

        public ResponseBuilder<T> WithData(T data)
        {
            _data = data;
            return this;
        }

        public ResponseBuilder<T> WithException(Exception exception)
        {
            _exception = exception;
            _errors.Add(new ErrorModel { Message = "exMessage:" + exception.Message + "ex.InnerException:" + exception.InnerException + "ex.StackTrace:" + exception.StackTrace });
            return this;
        }

        public bool IsSuccess { get => ((_errors == null || _errors.Count == 0) && _exception == null) ? true : false; }

        public IResponse<T> Build()
        {
            _isSuccess = ((_errors == null || _errors.Count == 0) && _exception == null);
            _response.IsSuccess = _isSuccess;
            _response.Errors = _errors;
            _response.Data = _data;
            return _response;
        }
    }
}

