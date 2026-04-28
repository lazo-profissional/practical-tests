using MediatR;
using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Mapping;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.Orders.Queries;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(cancellationToken);
        return orders.Select(o => o.ToDto()).ToList();
    }
}