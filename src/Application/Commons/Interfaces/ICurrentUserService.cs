namespace Application.Commons.Interfaces;

public interface ICurrentUserService
{
    string UserId { get; }

    string UserName { get; }

    List<string> Roles { get; }

    List<string> Chains { get; }
}