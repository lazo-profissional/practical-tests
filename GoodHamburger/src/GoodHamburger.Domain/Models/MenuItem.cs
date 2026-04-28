using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Models;

public class MenuItem
{
    public int Id { get; private set; }
    public MenuItemName Name { get; private set; }
    public decimal Price { get; private set; }
    public MenuItemType Type { get; private set; }

    private MenuItem() { }

    public MenuItem(int id, MenuItemName name, decimal price, MenuItemType type)
    {
        Id = id;
        Name = name;
        Price = price;
        Type = type;
    }

    public static List<MenuItem> GetFullMenu()
    {
        return new List<MenuItem>
        {
            new(1, MenuItemName.XBurger, 5.00m, MenuItemType.Sandwich),
            new(2, MenuItemName.XEgg, 4.50m, MenuItemType.Sandwich),
            new(3, MenuItemName.XBacon, 7.00m, MenuItemType.Sandwich),
            new(4, MenuItemName.Fries, 2.00m, MenuItemType.Side),
            new(5, MenuItemName.Soda, 2.50m, MenuItemType.Drink)
        };
    }
}