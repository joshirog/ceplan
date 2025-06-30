namespace Web.Features.Auth.SignIn;

public class SignInRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class SignInResponse
{
    public string Type { get; set; }
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
}