using Domain.Entities;

namespace Application.Commons.Interfaces;

public interface IJwtService
{
    Task<string> CreateAccessToken(User user, DateTime expiration);
}