using MediatR;
using GoodHamburger.Application.DTOs;

namespace GoodHamburger.Application.Orders.Queries;

public record GetOrdersQuery : IRequest<List<OrderDto>>;