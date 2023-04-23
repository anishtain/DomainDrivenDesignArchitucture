using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenDesignArchitucture.Presentation.Api.commons.endpoints;

[ApiVersion("1.0")]
[Route("api/v{version}/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
