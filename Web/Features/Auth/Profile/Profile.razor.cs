namespace Web.Features.Auth.Profile;
using Microsoft.AspNetCore.Components;

public class ProfileBase : ComponentBase
{
    [Inject] protected IProfileService ProfileService { get; set; }
    protected ProfileResponse Model { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Model = await ProfileService.Invoke();
    }
}