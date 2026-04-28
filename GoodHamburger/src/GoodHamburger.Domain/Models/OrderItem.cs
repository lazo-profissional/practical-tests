using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Models;

public class OrderItem
{
    public int Id { get; private set; }
    public int MenuItemId { get; private set; }
    public MenuItemName MenuItemName { get; private set; }
    public decimal Price { get; private set; }
    public MenuItemType Type { get; private set; }

    private OrderItem() { }

    public OrderItem(int menuItemId, MenuItemName menuItemName, decimal price, MenuItemType type)
    {
        MenuItemId = menuItemId;
        MenuItemName = menuItemName;
        Price = price;
        Type = type;
    }
}