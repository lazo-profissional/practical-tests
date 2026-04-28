namespace GoodHamburger.Blazor.Services;

public class ToastService
{
    public string? Message { get; private set; }
    public string ToastClass { get; private set; } = "text-bg-success";
    public event Action? OnChange;

    public void ShowSuccess(string message)
    {
        Message = message;
        ToastClass = "text-bg-success";
        OnChange?.Invoke();
    }

    public void ShowError(string message)
    {
        Message = message;
        ToastClass = "text-bg-danger";
        OnChange?.Invoke();
    }

    public void Clear()
    {
        Message = null;
        OnChange?.Invoke();
    }
}