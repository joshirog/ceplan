
using Application.Commons.Models;
using MediatR;

namespace Application.Features.Auth.Confirm
{
    public class ConfirmCommand : IRequest<Response<ConfirmResponse>>
    {
        public string UserId { get; set; }

        public string Token { get; set; }
    }
}