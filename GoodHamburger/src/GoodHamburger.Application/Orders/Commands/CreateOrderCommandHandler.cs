using MediatR;
using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Mapping;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Models;

namespace GoodHamburger.Application.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMenuRepository _menuRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IMenuRepository menuRepository)
    {
        _orderRepository = orderRepository;
        _menuRepository = menuRepository;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (request.MenuItemIds == null || request.MenuItemIds.Count == 0)
            throw new InvalidOperationException("At least one menu item is required.");

        var menuItems = new List<MenuItem>();
        foreach (var id in request.MenuItemIds)
        {
            var menuItem = await _menuRepository.GetByIdAsync(id, cancellationToken);
            if (menuItem == null)
                throw new InvalidOperationException($"Menu item with ID {id} not found.");

            menuItems.Add(menuItem);
        }

        var orderItems = menuItems.Select(m => new OrderItem(m.Id, m.Name, m.Price, m.Type)).ToList();

        var order = new Order(orderItems);
        var createdOrder = await _orderRepository.AddAsync(order, cancellationToken);

        return createdOrder.ToDto();
    }
}