using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Commons.Constants;
using Application.Commons.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly UserManager<User> _userManager;
    private readonly IApplicationDbContext _context;
    
    public JwtService(UserManager<User> userManager, IApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    
    public async Task<string> CreateAccessToken(User user, DateTime expiration)
    {
        var keyBytes = Encoding.UTF8.GetBytes(ConfigurationConstant.GetJwtKey);
        var symmetricKey = new SymmetricSecurityKey(keyBytes);

        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var claims = (await _userManager.GetClaimsAsync(user)).ToList();
        claims.Add(new Claim("id", user.Id.ToString()));
        claims.Add(new Claim("username", user.UserName));
        claims.Add(new Claim("email", user.Email));
        claims.Add(new Claim("phone", user.PhoneNumber));
        claims.Add(new Claim("sub", user.UserName));
        claims.Add(new Claim("aud", ConfigurationConstant.GetJwtAudience));
        
        //var roleClaims = permissions.Select(x => new Claim("role", x));
        //claims.AddRange(roleClaims);

        var token = new JwtSecurityToken(
            issuer: ConfigurationConstant.GetJwtIssuer,
            audience: ConfigurationConstant.GetJwtAudience,
            claims: claims,
            expires: expiration,
            signingCredentials: signingCredentials);

        var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
        
        return rawToken;
    }
}