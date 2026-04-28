using MediatR;
using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Mapping;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.Orders.Queries;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
        return order?.ToDto();
    }
}