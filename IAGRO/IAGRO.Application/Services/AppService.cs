using IAGRO.Application.Extentions;

namespace IAGRO.Application.Services;

public class AppService
{
    private List<string>? Errors = [];
    protected Response<T> Result<T>() => new Response<T>(true, default, Errors);
    protected Response<T> Result<T>(T? data) => new Response<T>(true, data, Errors);
    protected Response<T> Result<T>(string errorMessage)
    {
        Errors!.Add(errorMessage);
        return new Response<T>(false, default, Errors);
    }
    protected Response<T> Result<T>(List<string> errorMessage)
    {
        Errors = errorMessage;
        return new Response<T>(false, default, Errors);
    }
}