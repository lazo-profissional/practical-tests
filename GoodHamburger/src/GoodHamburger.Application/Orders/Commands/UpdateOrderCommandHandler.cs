using MediatR;
using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Mapping;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Models;

namespace GoodHamburger.Application.Orders.Commands;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMenuRepository _menuRepository;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMenuRepository menuRepository)
    {
        _orderRepository = orderRepository;
        _menuRepository = menuRepository;
    }

    public async Task<OrderDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var existingOrder = await _orderRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new EntityNotFoundException("Order", request.Id);

        if (request.MenuItemIds == null || request.MenuItemIds.Count == 0)
            throw new InvalidOperationException("At least one menu item is required.");

        var menuItems = new List<MenuItem>();
        foreach (var id in request.MenuItemIds)
        {
            var menuItem = await _menuRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new EntityNotFoundException("MenuItem", id);
            menuItems.Add(menuItem);
        }

        var orderItems = menuItems.Select(m => new OrderItem(m.Id, m.Name, m.Price, m.Type)).ToList();

        existingOrder.UpdateItems(orderItems);
        var updatedOrder = await _orderRepository.UpdateAsync(existingOrder, cancellationToken);

        return updatedOrder.ToDto();
    }
}