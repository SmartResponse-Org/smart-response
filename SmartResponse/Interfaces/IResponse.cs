using SmartResponse.Models;
using System.Collections.Generic;

namespace SmartResponse.Interfaces
{
    public interface IResponse<T>
    {
        bool IsSuccess { get; }

        List<ErrorModel> Errors { get; }
        
        T Data { get; }

        IResponse<T> Build(bool isSuccess, List<ErrorModel> errors, T data); 
    }
}
