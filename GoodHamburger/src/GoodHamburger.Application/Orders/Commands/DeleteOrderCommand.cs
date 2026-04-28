using MediatR;

namespace GoodHamburger.Application.Orders.Commands;

public record DeleteOrderCommand : IRequest
{
    public int Id { get; init; }
}