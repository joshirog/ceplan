using Application.Commons.Interfaces;
using MediatR;

namespace Application.Commons.Behaviours;

public class AccessBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;

    public AccessBehaviour(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is IAllowAuthorization)
        {
            return await next();
        }

        if (_currentUserService.UserId is null)
        {
            throw new UnauthorizedAccessException();
        }
        
        return await next();
    }
}