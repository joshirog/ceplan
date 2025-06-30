namespace Application.Features.Auth.SignIn.Commands;

public class SignInResponse
{
    public string Type { get; set; }
    
    public string AccessToken { get; set; }

    public DateTime Expiration { get; set; }
}