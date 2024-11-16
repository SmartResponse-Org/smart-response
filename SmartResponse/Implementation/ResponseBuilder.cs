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
    public class ResponseBuilder<T> : IResponseBuilder<T>
    {
        private bool _isSuccess;
        private T _data;
        private List<ErrorModel> _errors;
        private Exception _exception;
        private IResponse<T> _response;
        private ICustomStringLocalizer<ErrorMessage> _messageLocalizer;
        private ICustomStringLocalizer<Label> _labelLocalizer;

        public ResponseBuilder(IResponse<T> response,
                               ICustomStringLocalizer<ErrorMessage> messageLocalizer,
                               ICustomStringLocalizer<Label> labelLocalizer)
        {
            _response = response;
            _errors = new List<ErrorModel>();
            _messageLocalizer = messageLocalizer;
            _labelLocalizer = labelLocalizer;
        }

        // Set Errors.
        public IResponse<T> Set(MessageCode code, string? fieldName, params string[] labels)
        {
            return Append(code, fieldName, labels)
                .Build();
        }

        public IResponse<T> Set<Label>(MessageCode code, string? fieldName, params string[] labels)
        {
            return Append<Label>(code, fieldName, labels)
                .Build();
        }

        public IResponse<T> Set<Error>(string code, string? fieldName, params string[] labels)
        {
            return Append<Error>(code, fieldName, labels)
                .Build();   
        }

        public IResponse<T> Set<Error>(string code, string? fieldName)
        {
            return Append<Error>(code, fieldName)
                .Build();
        }

        public IResponse<T> Set<Error, Label>(string code, string? fieldName, params string[] labels)
        {
            return Append<Error, Label>(code, fieldName, labels)
                .Build();
        }

        public IResponse<T> Set(List<ValidationFailure> errors)
        {
            return Append(errors)
                .Build();
        }

        public IResponse<T> Set(ErrorModel error)
        {
            return Append(error)
                .Build();
        }

        public IResponse<T> Set(List<ErrorModel> errors)
        {
            return Append(errors)
                .Build();
        }

        public IResponse<T> Set(Exception exception)
        {
            return Append(exception)
                .Build();
        }

        // Append Errors.
        public IResponseBuilder<T> Append(MessageCode code, string? fieldName, params string[] labels)
        {
            string msg = _messageLocalizer[code.GetDescription()];

            if (labels.Any())
            {
                var localizedLabels = new List<string>();

                foreach (var label in labels)
                {
                    localizedLabels.Add(_labelLocalizer[label]);
                }

                msg = _messageLocalizer[code.GetDescription(), localizedLabels.ToArray()];
            }

            _errors.Add(new ErrorModel
            {
                FieldName = fieldName,
                Code = code.GetDescription(),
                Message = msg
            });

            return this;
        }

        public IResponseBuilder<T> Append<Label>(MessageCode code, string? fieldName, params string[] labels)
        {
            var labelLocalizer = LocalizerProvider<Label>.GetLocalizer();

            string msg = _messageLocalizer[code.GetDescription()];

            if (labels.Any())
            {
                var localizedLabels = new List<string>();

                foreach (var label in labels)
                {
                    localizedLabels.Add(labelLocalizer[label]);
                }

                msg = _messageLocalizer[code.GetDescription(), localizedLabels.ToArray()];
            }

            _errors.Add(new ErrorModel
            {
                FieldName = fieldName,
                Code = code.GetDescription(),
                Message = msg
            });

            return this;
        }

        public IResponseBuilder<T> Append<Error>(string code, string? fieldName, params string[] labels)
        {
            var messageLocalizer = LocalizerProvider<Error>.GetLocalizer();

            string msg = messageLocalizer[code];

            if (labels.Any())
            {
                var localizedLabels = new List<string>();

                foreach (var label in labels)
                {
                    localizedLabels.Add(_labelLocalizer[label]);
                }

                msg = messageLocalizer[code, localizedLabels.ToArray()];
            }

            _errors.Add(new ErrorModel
            {
                FieldName = fieldName,
                Code = code,
                Message = msg
            });

            return this;
        }

        public IResponseBuilder<T> Append<Error>(string code, string? fieldName)
        {
            var messageLocalizer = LocalizerProvider<Error>.GetLocalizer();

            string msg = messageLocalizer[code];

            _errors.Add(new ErrorModel
            {
                FieldName = fieldName,
                Code = code,
                Message = msg
            });

            return this;
        }

        public IResponseBuilder<T> Append<Error, Label>(string code, string? fieldName, params string[] labels)
        {
            var messageLocalizer = LocalizerProvider<Error>.GetLocalizer();
            var labelLocalizer = LocalizerProvider<Label>.GetLocalizer();

            string msg = messageLocalizer[code];

            if (labels.Any())
            {
                var localizedLabels = new List<string>();

                foreach (var label in labels)
                {
                    localizedLabels.Add(labelLocalizer[label]);
                }

                msg = messageLocalizer[code, localizedLabels.ToArray()];
            }

            _errors.Add(new ErrorModel
            {
                FieldName = fieldName,
                Code = code,
                Message = msg
            });

            return this;
        }

        public IResponseBuilder<T> Append(List<ValidationFailure> errors)
        {
            foreach (var item in errors)
            {
                _errors.Add(new ErrorModel

                {
                    FieldName = item.PropertyName,
                    Code = item.ErrorCode,
                    Message = item.ErrorMessage,
                });
            }

            return this;
        }

        public IResponseBuilder<T> Append(ErrorModel error)
        {
            _errors.Add(error);

            return this;
        }

        public IResponseBuilder<T> Append(List<ErrorModel> errors)
        {
            _errors.AddRange(errors);

            return this;
        }

        public IResponseBuilder<T> Append(Exception exception)
        {
            _exception = exception;
            _errors.Add(new ErrorModel { Message = "exMessage:" + exception.Message + "ex.InnerException:" + exception.InnerException + "ex.StackTrace:" + exception.StackTrace });

            return this;
        }

        public IResponse<T> Build(T data)
        {
            _data = data;

            return Build();
        }

        public IResponse<T> Build()
        {
            _isSuccess = ((_errors == null || !_errors.Any()) && _exception == null);

            return _response.Build(_isSuccess, _errors, _data);
        }
    }
}
