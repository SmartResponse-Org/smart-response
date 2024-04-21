using SmartResponse.Interfaces;
using SmartResponse.Models;
using System.Collections.Generic;

namespace SmartResponse.Implementation
{
    public class Response<T> : IResponse<T>
    {
        public bool IsSuccess { get; private set; }

        public List<ErrorModel> Errors { get; private set; }

        public T Data { get; private set; }

        public IResponse<T> Build(bool isSuccess, List<ErrorModel> errors, T data)
        {
            IsSuccess = isSuccess;
            Errors = errors;
            Data = data;

            return this;
        }
    }
}
