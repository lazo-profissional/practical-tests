using MediatR;
using GoodHamburger.Application.DTOs;

namespace GoodHamburger.Application.Orders.Commands;

public record CreateOrderCommand : IRequest<OrderDto>
{
    public List<int> MenuItemIds { get; init; } = new();
}