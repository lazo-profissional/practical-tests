using MediatR;
using GoodHamburger.Application.DTOs;

namespace GoodHamburger.Application.Orders.Commands;

public record UpdateOrderCommand : IRequest<OrderDto>
{
    public int Id { get; init; }
    public List<int> MenuItemIds { get; init; } = new();
}