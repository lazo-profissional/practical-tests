using MediatR;
using GoodHamburger.Application.DTOs;

namespace GoodHamburger.Application.Orders.Queries;

public record GetOrderByIdQuery : IRequest<OrderDto?>
{
    public int Id { get; init; }
}