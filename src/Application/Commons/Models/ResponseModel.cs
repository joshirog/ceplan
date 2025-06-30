using Application.Commons.Constants;

namespace Application.Commons.Models;

public static class Response
{
    public static Response<T> Fail<T>(string message) => new(message);
    public static Response<T> Fail<T>(string message, T data) => new(data, message, false);
    public static Response<T> Error<T>(string exception, List<string> errors) => new(false, exception, errors);
    public static Response<T> Ok<T>(string message, T data = default) => new(data, message ?? "Success", true);
    public static Response<T> Ok<T>(T data = default) => new(data, "Success", true);
}

public class Response<T>
{
    public Response(T data, string message, bool success)
    {
        Message = message;
        Data = data;
        Success = success;
    }

    public Response(bool success, string exception = null, List<string> errors = null)
    {
        Success = success;
        Message = ResponseConstant.Error;
        Exception = exception;
        Errors = errors;
    }
    
    public Response(string message)
    {
        Message = message;
        Success = false;
    }

    public bool Success { get; set; }
    
    public T Data { get; set; }
    
    public string Message { get; set; }
    
    public string Exception { get; set; }
    
    public List<string> Errors { get; set; }
}