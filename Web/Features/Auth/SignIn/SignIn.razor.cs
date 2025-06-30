using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;  

namespace Web.Features.Auth.SignIn;

public class SignInBase : ComponentBase
{
    [Inject] protected ISignInService SignInService { get; set; } = default!;
    [Inject] protected NavigationManager Navigation { get; set; } = default!;
    [Inject] protected IJSRuntime JS { get; set; } = default!;
    protected SignInRequest Model { get; set; } = new();
    protected string? ErrorMessage;
    protected List<string>? Errors;
    protected bool Success = true;
    protected bool Loading;

    protected async Task HandleLogin()
    {
        Loading = true;
        ErrorMessage = null;

        var result = await SignInService.Invoke(Model);
        Success = result.Success;
        if (result.Success)
        {
            if (!string.IsNullOrEmpty(result.Data.AccessToken))
            {
                await JS.InvokeVoidAsync("localStorage.setItem", "authToken", result.Data.AccessToken);
            }
            Navigation.NavigateTo("/profile");
        }
        else
        {
            ErrorMessage = result.Message ?? "Error al iniciar sesión.";
            Errors = result.Errors;
        }

        Loading = false;
    }
}