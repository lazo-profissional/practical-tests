using MediatR;
using Microsoft.AspNetCore.Mvc;
using GoodHamburger.Application.Menu.Queries;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMediator _mediator;

    public MenuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetMenu(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetMenuQuery(), cancellationToken);
        return Ok(result);
    }
}