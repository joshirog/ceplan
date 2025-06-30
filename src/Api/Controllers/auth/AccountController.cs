using System.Threading.Tasks;
using Api.Controllers.Bases;
using Application.Features.Auth.Confirm;
using Application.Features.Auth.Me.Queries;
using Application.Features.Auth.SignIn.Commands;
using Application.Features.Auth.SignUp.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers.auth;

[Route("api/auth/[controller]")]
public class AccountController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpPost("SignUp")]
    [SwaggerOperation(Summary = "Create new user")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command) => Ok(await Mediator.Send(command));
    
    [AllowAnonymous]
    [HttpPost("Confirm")]
    [SwaggerOperation(Summary = "Activate account")]
    public async Task<IActionResult> Confirm([FromBody] ConfirmCommand command) => Ok(await Mediator.Send(command));
    
    [AllowAnonymous]
    [HttpPost("SignIn")]
    [SwaggerOperation(Summary = "Login and jwt generation")]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand command) => Ok(await Mediator.Send(command));
    
    [HttpGet("Me")]
    [SwaggerOperation(Summary = "Get profile user")]
    public async Task<IActionResult> Me() => Ok(await Mediator.Send(new GetMeQuery()));
}