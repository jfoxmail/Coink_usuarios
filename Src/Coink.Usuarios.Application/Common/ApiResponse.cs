namespace Coink.Usuarios.Application.Common
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public object? Errors { get; set; }

        public static ApiResponse<T> Success(T data, int statusCode = 200)
            => new ApiResponse<T> { Data = data, StatusCode = statusCode };

        public static ApiResponse<T> Fail(object errors, int statusCode = 400)
            => new ApiResponse<T> { Errors = errors, StatusCode = statusCode };
    }
}
