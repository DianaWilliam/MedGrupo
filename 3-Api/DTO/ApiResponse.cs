namespace MedGrupo.Api.DTO;

public class ApiResponse<T>
{
    public T? Result { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    public ApiResponse(T result) => this.Result = result;
    public ApiResponse(IEnumerable<string> errors) => this.Errors = errors;
    public ApiResponse(T result, IEnumerable<string> errors)
    {
        this.Result = result;
        this.Errors = errors;
    }
}