using Microsoft.AspNetCore.Mvc;

namespace Tempo.Api.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { Message = "Success!" });
}