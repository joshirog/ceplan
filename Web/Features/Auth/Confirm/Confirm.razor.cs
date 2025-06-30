using Microsoft.AspNetCore.Components;

namespace Web.Features.Auth.Confirm;

public class ConfirmBase : ComponentBase
{
    [Inject] protected NavigationManager Navigation { get; set; } = default!;
    [Inject] protected IConfirmService ConfirmService { get; set; } = default!;

    [Parameter] public Guid userId { get; set; }
    [Parameter] [SupplyParameterFromQuery] public string? token { get; set; }

    protected bool _confirmed = false;
    protected bool _loaded = false;
    protected string? _message;
    protected string? _userName;

    protected override async Task OnInitializedAsync()
    {
        if (string.IsNullOrWhiteSpace(token) || userId == Guid.Empty)
        {
            _message = "Token o usuario inválido.";
            _loaded = true;
            return;
        }

        var result = await ConfirmService.Invoke(userId, token);

        _confirmed = result.Success && result.Data?.Id != Guid.Empty;
        _userName = result.Data?.Name;
        _message = result.Message ?? "Error al confirmar la cuenta.";
        _loaded = true;
    }

    protected void GoToLogin() => Navigation.NavigateTo("/account/signin");
}