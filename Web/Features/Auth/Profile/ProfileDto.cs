namespace Web.Features.Auth.Profile;

public class ProfileResponse
{
    public string id { get; set; }
    public string documentType { get; set; }
    public string userName { get; set; }
    public string name { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public RoleModel role { get; set; }
}

public class RoleModel
{
    public string id { get; set; }
    public string name { get; set; }
}