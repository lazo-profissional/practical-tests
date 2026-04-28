using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Models;
using Xunit;

namespace GoodHamburger.Tests.Domain;

public class UpdateOrderTests
{
    [Fact]
    public void UpdateItems_WithValidNewItems_ShouldRecalculateTotal()
    {
        var originalItems = new List<OrderItem>
        {
            new(1, MenuItemName.XBurger, 5.00m, MenuItemType.Sandwich),
            new(4, MenuItemName.Fries, 2.00m, MenuItemType.Side)
        };
        var order = new Order(originalItems);
        Assert.Equal(6.30m, order.Total);

        var newItems = new List<OrderItem>
        {
            new(1, MenuItemName.XBurger, 5.00m, MenuItemType.Sandwich),
            new(4, MenuItemName.Fries, 2.00m, MenuItemType.Side),
            new(5, MenuItemName.Soda, 2.50m, MenuItemType.Drink)
        };
        order.UpdateItems(newItems);

        Assert.Equal(9.50m, order.Subtotal);
        Assert.Equal(1.90m, order.Discount);
        Assert.Equal(7.60m, order.Total);
    }

    [Fact]
    public void UpdateItems_WithDuplicateItems_ShouldThrowException()
    {
        var originalItems = new List<OrderItem>
        {
            new(1, MenuItemName.XBurger, 5.00m, MenuItemType.Sandwich)
        };
        var order = new Order(originalItems);

        var duplicateItems = new List<OrderItem>
        {
            new(1, MenuItemName.XBurger, 5.00m, MenuItemType.Sandwich),
            new(1, MenuItemName.XBurger, 5.00m, MenuItemType.Sandwich)
        };

        var exception = Assert.Throws<InvalidOperationException>(() => order.UpdateItems(duplicateItems));
        Assert.Contains("Duplicate item detected", exception.Message);
    }
}