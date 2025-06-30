using System.Text;
using Application.Commons.Constants;
using Application.Commons.Interfaces;
using Application.Commons.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Confirm
{
    public class ConfirmHandler(IMediator mediator, UserManager<User> userManager, IApplicationDbContext context, IMapper mapper) : IRequestHandler<ConfirmCommand, Response<ConfirmResponse>>
    {
        public async Task<Response<ConfirmResponse>> Handle(ConfirmCommand request, CancellationToken cancellationToken)
        {
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(request.Token);
            var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);
            
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id.Equals(Guid.Parse(request.UserId)), cancellationToken);

            if(user is null)
                return Response.Fail(ResponseConstant.Error, new ConfirmResponse());
            
            var result = await userManager.ConfirmEmailAsync(user, tokenDecoded);

            if (!result.Succeeded) 
                return Response.Fail(ResponseConstant.Error, new ConfirmResponse());
            
            await mediator.Publish(new ConfirmNotification{ User = user }, cancellationToken);

            return Response.Ok(ResponseConstant.ActivationSuccess,  mapper.Map<ConfirmResponse>(user));
        }
    }
}