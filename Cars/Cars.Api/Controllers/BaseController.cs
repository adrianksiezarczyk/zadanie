using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Api.Controllers;

[Produces("application/json")]
[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    private ISender? mediator;
    protected ISender Mediator => mediator ??= HttpContext.RequestServices.GetService<ISender>()!;
}
