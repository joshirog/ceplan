using Microsoft.Extensions.Configuration;

namespace Application.Commons.Constants;

public static class ConfigurationConstant
{
    public static bool IsLocal;
    
    public static string GetEnvironment { get; private set; }
    
    public static string GetApplicationName { get; private set; }

    public static string GetIntranetDomain { get; private set; }

    public static string GetBcc { get; private set; }

    public static string GetConnectionString { get; private set; }
    
    public static bool GetIdentityRequireUniqueEmail { get; private set; }
    
    public static int GetIdentityDefaultLockoutTimeSpan { get; private set; }
    
    public static int GetIdentityMaxFailedAccessAttempts { get; private set; }
    
    public static int GetIdentityPasswordRequiredLength { get; private set; }
    
    public static int GetIdentityPasswordRequiredUniqueChars { get; private set; }
    
    public static bool GetIdentityPasswordRequireDigit { get; private set; }
    
    public static bool GetIdentityPasswordRequireNonAlphanumeric { get; private set; }
    
    public static bool GetIdentityPasswordRequireUppercase { get; private set; }
    
    public static bool GetIdentitySignInRequireConfirmedEmail { get; private set; }
    
    public static string GetIdentityEmailConfirmationTokenProvider { get; private set; }
    
    public static int GetIdentityTokenLifespan { get; private set; }
    
    public static string GetJwtIssuer { get; private set; }
    
    public static string GetJwtAudience { get; private set; }
    
    public static string GetJwtKey { get; private set; }
    
    public static int GetJwtExpiration { get; private set; }

    public static string GetSendInBlueHost { get; set; }
    
    public static string GetSendInBlueApiKey { get; set; }

    public static void Load(IConfiguration configuration)
    {
        GetEnvironment = configuration.GetValue<string>("AppSettings:Environment");
        
        IsLocal = GetEnvironment!.ToLower().Equals(GlobalConstant.EnvironmentLocal);

        GetApplicationName = configuration.GetValue<string>("AppSettings:ApplicationName");
        
        GetIntranetDomain = configuration.GetValue<string>("AppSettings:IntranetDomain");
        
        GetBcc = configuration.GetValue<string>("AppSettings:Bcc");

        GetConnectionString = configuration.GetConnectionString("default");
        
        GetIdentityRequireUniqueEmail = configuration.GetValue<bool>("Identity:IdentityRequireUniqueEmail");

        GetIdentityDefaultLockoutTimeSpan = configuration.GetValue<int>("Identity:DefaultLockoutTimeSpan");
        
        GetIdentityMaxFailedAccessAttempts = configuration.GetValue<int>("Identity:MaxFailedAccessAttempts");
        
        GetIdentityPasswordRequiredLength = configuration.GetValue<int>("Identity:PasswordRequiredLength");
        
        GetIdentityPasswordRequiredUniqueChars = configuration.GetValue<int>("Identity:PasswordRequiredUniqueChars");
        
        GetIdentityPasswordRequireDigit = configuration.GetValue<bool>("Identity:PasswordRequireDigit");
        
        GetIdentityPasswordRequireNonAlphanumeric = configuration.GetValue<bool>("Identity:GetPasswordRequireNonAlphanumeric");
        
        GetIdentityPasswordRequireUppercase = configuration.GetValue<bool>("Identity:PasswordRequireUppercase");
        
        GetIdentitySignInRequireConfirmedEmail = configuration.GetValue<bool>("Identity:SignInRequireConfirmedEmail");
        
        GetIdentityEmailConfirmationTokenProvider = configuration.GetValue<string>("Identity:EmailConfirmationTokenProvider");
        
        GetIdentityTokenLifespan = configuration.GetValue<int>("Identity:TokenLifespan");
        
        GetJwtIssuer = configuration.GetValue<string>("Jwt:Issuer");
        
        GetJwtAudience = configuration.GetValue<string>("Jwt:Audience");
        
        GetJwtKey = configuration.GetValue<string>("Jwt:Key");
        
        GetJwtExpiration = configuration.GetValue<int>("Jwt:Expiration");
        
        GetSendInBlueHost = configuration.GetValue<string>("SendInBlue:BaseAddress");
        
        GetSendInBlueApiKey = configuration.GetValue<string>("SendInBlue:ApiKey");
        
        if (IsLocal) Print();
    }

    private static void Print()
    {
        Console.WriteLine($"Environment: {GetEnvironment}");
        Console.WriteLine($"IsLocal: {IsLocal}");
        Console.WriteLine($"ApplicationName: {GetApplicationName}");
        Console.WriteLine($"ConnectionString: {GetConnectionString}");
        Console.WriteLine($"IdentityRequireUniqueEmail: {GetIdentityRequireUniqueEmail}");
        Console.WriteLine($"IdentityDefaultLockoutTimeSpan: {GetIdentityDefaultLockoutTimeSpan}");
        Console.WriteLine($"IdentityMaxFailedAccessAttempts: {GetIdentityMaxFailedAccessAttempts}");
        Console.WriteLine($"GetIdentityPasswordRequiredLength: {GetIdentityPasswordRequiredLength}");
        Console.WriteLine($"GetIdentityPasswordRequiredUniqueChars: {GetIdentityPasswordRequiredUniqueChars}");
        Console.WriteLine($"GetIdentityPasswordRequireDigit: {GetIdentityPasswordRequireDigit}");
        Console.WriteLine($"GetIdentityPasswordRequireNonAlphanumeric: {GetIdentityPasswordRequireNonAlphanumeric}");
        Console.WriteLine($"GetIdentityPasswordRequireUppercase: {GetIdentityPasswordRequireUppercase}");
        Console.WriteLine($"GetIdentitySignInRequireConfirmedEmail: {GetIdentitySignInRequireConfirmedEmail}");
        Console.WriteLine($"GetIdentityEmailConfirmationTokenProvider: {GetIdentityEmailConfirmationTokenProvider}");
        Console.WriteLine($"GetIdentityTokenLifespan: {GetIdentityTokenLifespan}");
        Console.WriteLine($"GetSendInBlueHost: {GetSendInBlueHost}");
        Console.WriteLine($"GetSendInBlueApiKey: {GetSendInBlueApiKey}");
    }
}