namespace Web.Features.Auth.Confirm;

public class ConfirmResponse
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
}