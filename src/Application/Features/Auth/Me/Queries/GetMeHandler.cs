using Application.Commons.Constants;
using Application.Commons.Interfaces;
using Application.Commons.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Me.Queries;

public class GetProfileHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper) : IRequestHandler<GetMeQuery, Response<GetMeResponse>>
{
    public async Task<Response<GetMeResponse>> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .AsNoTracking()
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id.Equals(Guid.Parse(currentUserService.UserId)), cancellationToken);
        
        var response = mapper.Map<GetMeResponse>(user);
        response.Role = mapper.Map<GetMeRoleResponse>(user.UserRoles.First().Role);
        
        return Response.Ok(ResponseConstant.Success, response);
    }
}