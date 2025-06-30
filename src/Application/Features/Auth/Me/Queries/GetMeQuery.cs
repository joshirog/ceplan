using Application.Commons.Models;
using MediatR;

namespace Application.Features.Auth.Me.Queries;

public class GetMeQuery : IRequest<Response<GetMeResponse>>;