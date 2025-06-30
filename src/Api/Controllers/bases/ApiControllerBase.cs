using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Controllers.Bases;

[Authorize]
[Produces("application/json")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    private ISender _mediator;
    
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}