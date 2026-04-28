using Xunit;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Models;

namespace GoodHamburger.Tests.Domain;

public class OrderTests
{
    [Fact]
    public void CreateOrder_WithValidItems_ShouldSetCorrectSubtotal()
    {
        var items = new List<OrderItem>
        {
            new(1, MenuItemName.XBurger, 5.00m, MenuItemType.Sandwich),
            new(4, MenuItemName.Fries, 2.00m, MenuItemType.Side),
            new(5, MenuItemName.Soda, 2.50m, MenuItemType.Drink)
        };
        var order = new Order(items);
        Assert.Equal(9.50m, order.Subtotal);
    }

    [Fact]
    public void CreateOrder_WithNoItems_ShouldThrowException()
    {
        Assert.Throws<InvalidOperationException>(() => new Order(new List<OrderItem>()));
    }

    [Fact]
    public void CreateOrder_WithDuplicateItems_ShouldThrowException()
    {
        var items = new List<OrderItem>
        {
            new(1, MenuItemName.XBurger, 5.00m, MenuItemType.Sandwich),
            new(1, MenuItemName.XBurger, 5.00m, MenuItemType.Sandwich)
        };
        var ex = Assert.Throws<InvalidOperationException>(() => new Order(items));
        Assert.Contains("Duplicate item detected", ex.Message);
    }

    [Fact]
    public void CreateOrder_WithTwoSandwiches_ShouldThrowException()
    {
        var items = new List<OrderItem>
        {
            new(1, MenuItemName.XBurger, 5.00m, MenuItemType.Sandwich),
            new(2, MenuItemName.XEgg, 4.50m, MenuItemType.Sandwich)
        };
        var ex = Assert.Throws<InvalidOperationException>(() => new Order(items));
        Assert.Contains("Maximum 1 sandwich", ex.Message);
    }

    [Fact]
    public void CreateOrder_WithTwoSides_ShouldThrowException()
    {
        var items = new List<OrderItem>
        {
            new(1, MenuItemName.XBurger, 5.00m, MenuItemType.Sandwich),
            new(4, MenuItemName.Fries, 2.00m, MenuItemType.Side),
            new(4, MenuItemName.Fries, 2.00m, MenuItemType.Side)
        };
        var ex = Assert.Throws<InvalidOperationException>(() => new Order(items));
        Assert.Contains("Duplicate item detected", ex.Message);
    }

    [Fact]
    public void CreateOrder_WithTwoDrinks_ShouldThrowException()
    {
        var items = new List<OrderItem>
        {
            new(1, MenuItemName.XBurger, 5.00m, MenuItemType.Sandwich),
            new(5, MenuItemName.Soda, 2.50m, MenuItemType.Drink),
            new(5, MenuItemName.Soda, 2.50m, MenuItemType.Drink)
        };
        var ex = Assert.Throws<InvalidOperationException>(() => new Order(items));
        Assert.Contains("Duplicate item detected", ex.Message);
    }
}