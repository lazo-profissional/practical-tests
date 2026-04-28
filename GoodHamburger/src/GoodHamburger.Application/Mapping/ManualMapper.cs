using GoodHamburger.Application.DTOs;
using GoodHamburger.Domain.Models;

namespace GoodHamburger.Application.Mapping;

public static class ManualMapper
{
    public static MenuItemDto ToDto(this MenuItem menuItem)
    {
        return new MenuItemDto
        {
            Id = menuItem.Id,
            Name = menuItem.Name.ToString(),
            Price = menuItem.Price,
            Type = menuItem.Type.ToString()
        };
    }

    public static OrderItemDto ToDto(this OrderItem orderItem)
    {
        return new OrderItemDto
        {
            MenuItemId = orderItem.MenuItemId,
            Name = orderItem.MenuItemName.ToString(),
            Price = orderItem.Price,
            Type = orderItem.Type.ToString()
        };
    }

    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            Items = order.Items.Select(i => i.ToDto()).ToList(),
            Subtotal = order.Subtotal,
            Discount = order.Discount,
            Total = order.Total,
            CreatedAt = order.CreatedAt
        };
    }
}