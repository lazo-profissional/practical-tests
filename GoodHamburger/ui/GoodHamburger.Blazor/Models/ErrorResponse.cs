namespace GoodHamburger.Blazor.Models;

public class ErrorResponse
{
    public string Error { get; set; } = string.Empty;
    public int StatusCode { get; set; }
}