using Application.Commons.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Bases;

[Route("")]
[ApiController]
public class ProbeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok($"{ConfigurationConstant.GetApplicationName} api is running ...");
    }
}