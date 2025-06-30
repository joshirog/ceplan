namespace Web.Features.Auth.SignUp;

public class SignUpRequest
{
    public string UserName { get; set; }

    public string Password { get; set; }
    
    public string ConfirmPassword { get; set; }

    public string DocumentType { get; set; }

    public string Name { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }
}

public class SignUpResponse
{
    public Guid Id { get; set; }
    
    public string UserName { get; set; } 

    public string Email { get; set; } 
}