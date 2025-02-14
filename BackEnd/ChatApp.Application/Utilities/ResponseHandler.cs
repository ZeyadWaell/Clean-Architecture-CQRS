using ChatApp.Application.Utilities.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Utilities
{
    public static class ResponseHandler
    {
        public static ApiResponse<T> Success<T>(T data, string message = "Operation succeeded.")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Failure<T>(List<string> errors, string message = "Operation failed.")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }

        public static ApiResponse<T> Failure<T>(string error, string message = "Operation failed.")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = new List<string> { error }
            };
        }
    }
}
