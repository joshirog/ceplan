namespace Web.Features.Auth.SignUp;

using Microsoft.AspNetCore.Components;


public class SignUpBase : ComponentBase
{
    [Inject] protected ISignUpService SignUpService { get; set; } = default!;
    [Inject] protected NavigationManager Navigation { get; set; } = default!;

    protected SignUpRequest Model = new();
    protected string? ErrorMessage;
    protected List<string>? Errors;
    protected bool Success = true;

    protected async Task HandleSignup()
    {
        var response = await SignUpService.Invoke(Model);
        Success = response.Success;
        if (response.Success)
        {
            Navigation.NavigateTo("/account/signin");
        }
        else
        {
            ErrorMessage = response.Message ?? "Error al iniciar sesión.";
            Errors = response.Errors;
        }
    }
}