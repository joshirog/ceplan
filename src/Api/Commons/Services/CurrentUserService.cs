using System.Collections.Generic;
using System.Security.Claims;
using Application.Commons.Constants;
using Application.Commons.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Api.Commons.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string UserName => httpContextAccessor.HttpContext?.User?.FindFirstValue("username") ?? GlobalConstant.DefaultUsername;
        
    public string UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue("id") ?? GlobalConstant.DefaultUserId;
        
    public List<string> Roles => new();
    
    public List<string> Chains => new();
}