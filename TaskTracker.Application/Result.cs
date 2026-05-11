namespace TaskTracker.Application
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T> { IsSuccess = true, Data = data };
        }

        public static Result<T> Failure(string errorMsg)
        {
            return new Result<T> { IsSuccess = false, Message = errorMsg };
        }
    }
}