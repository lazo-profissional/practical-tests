using MediatR;
using Microsoft.AspNetCore.Mvc;
using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Orders.Commands;
using GoodHamburger.Application.Orders.Queries;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
    {
        var orders = await _mediator.Send(new GetOrdersQuery(), cancellationToken);
        return Ok(orders);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderById(int id, CancellationToken cancellationToken)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery { Id = id }, cancellationToken);
        if (order == null)
            return NotFound(new { error = $"Order with ID {id} not found.", statusCode = 404 });

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand { MenuItemIds = dto.MenuItemIds };
        var order = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateOrderCommand { Id = id, MenuItemIds = dto.MenuItemIds };
        var order = await _mediator.Send(command, cancellationToken);

        return Ok(order);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteOrderCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}