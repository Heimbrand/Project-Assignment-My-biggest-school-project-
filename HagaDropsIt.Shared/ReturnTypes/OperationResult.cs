using System;

namespace HagaDropsIt.Shared.ReturnTypes
{
    public class OperationResult
    {
        public bool Success { get; protected set; } 
        public string ErrorMessage { get; protected set; } // Changed from private to protected

        public static OperationResult Fail(string errorMessage)
        {
            return new OperationResult { Success = false, ErrorMessage = errorMessage };
        }

        public static OperationResult Ok()
        {
            return new OperationResult { Success = true };
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; private set; }

        public static OperationResult<T> Fail(string errorMessage)
        {
            return new OperationResult<T> { Success = false, ErrorMessage = errorMessage };
        }

        public static OperationResult<T> Ok(T data)
        {
            return new OperationResult<T> { Success = true, Data = data };
        }
    }
}
