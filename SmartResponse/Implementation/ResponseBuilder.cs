using FluentValidation.Results;
using SmartResponse.Enums;
using SmartResponse.Interfaces;
using SmartResponse.Localization;
using SmartResponse.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SmartResponse.Implementation
{
    public class ResponseBuilder<T>
    {
        private bool _isSuccess;
        private T _data;
        private List<ErrorModel> _errors;
        private Exception _exception;
        private Response<T> _response;
        private ICustomStringLocalizer<ErrorMessage> _messageLocalizer;
        private ICustomStringLocalizer<Label> _labelLocalizer;

        public ResponseBuilder(Response<T> response, Culture culture)
        {
            //this.data = Activator.CreateInstance<T>();//TODO:recheck this
            _response = response;
            _errors = new List<ErrorModel>();
            _exception = null;

            var cultureInfo = new CultureInfo("en");

            switch (culture)
            {
                case Culture.ar:
                    cultureInfo = new CultureInfo("ar");
                    break;
            }

            _messageLocalizer = LocalizerProvider<ErrorMessage>.GetLocalizer(cultureInfo);
            _labelLocalizer = LocalizerProvider<Label>.GetLocalizer(cultureInfo);
        }

        public ResponseBuilder<T> AppendError(ErrorModel error)
        {
            _errors.Add(error);

            return this;
        }


        public ResponseBuilder<T> AppendError(MessageCode code, string? fieldName, params string[] labels)
        {
            string msg = _messageLocalizer[code.StringValue()];

            if (labels.Any())
            {
                var localizedLabels = new List<string>();

                foreach (var label in labels)
                {
                    localizedLabels.Add(_labelLocalizer[label]);
                }

                msg = _messageLocalizer[code.StringValue(), localizedLabels.ToArray()];
            }

            _errors.Add(new ErrorModel
            {
                FieldName = fieldName,
                Code = code.StringValue(),
                Message = msg
            });

            return this;
        }

        public ResponseBuilder<T> AppendError<Error, Label>(string code, string? fieldName, params string[] labels)
        {
            var _messageLocalizer = LocalizerProvider<Error>.GetLocalizer();
            var _labelLocalizer = LocalizerProvider<Label>.GetLocalizer();

            string msg = _messageLocalizer[code];

            if (labels.Any())
            {
                var localizedLabels = new List<string>();

                foreach (var label in labels)
                {
                    localizedLabels.Add(_labelLocalizer[label]);
                }

                msg = _messageLocalizer[code, localizedLabels.ToArray()];
            }

            _errors.Add(new ErrorModel
            {
                FieldName = fieldName,
                Code = code,
                Message = msg
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
                    FieldLang = item.ErrorCode == MessageCode.Required.StringValue() ? item.AttemptedValue?.ToString() : null
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

