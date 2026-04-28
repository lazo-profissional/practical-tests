using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Models;
using Xunit;

namespace GoodHamburger.Tests.Domain;

public class DiscountTests
{
    [Fact]
    public void Discount_SandwichSideDrink_ShouldBe20Percent()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(1, MenuItemName.XBurger, 5.00m, MenuItemType.Sandwich),
            new(4, MenuItemName.Fries, 2.00m, MenuItemType.Side),
            new(5, MenuItemName.Soda, 2.50m, MenuItemType.Drink)
        };

        // Act
        var order = new Order(items);

        // Assert
        Assert.Equal(9.50m, order.Subtotal);
        Assert.Equal(1.90m, order.Discount);  // 20% of 9.50
        Assert.Equal(7.60m, order.Total);
    }

    [Fact]
    public void Discount_SandwichDrink_ShouldBe15Percent()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(3, MenuItemName.XBacon, 7.00m, MenuItemType.Sandwich),
            new(5, MenuItemName.Soda, 2.50m, MenuItemType.Drink)
        };

        // Act
        var order = new Order(items);

        // Assert
        Assert.Equal(9.50m, order.Subtotal);
        Assert.Equal(1.425m, order.Discount);  // 15% of 9.50
        Assert.Equal(8.075m, order.Total);
    }

    [Fact]
    public void Discount_SandwichSide_ShouldBe10Percent()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(2, MenuItemName.XEgg, 4.50m, MenuItemType.Sandwich),
            new(4, MenuItemName.Fries, 2.00m, MenuItemType.Side)
        };

        // Act
        var order = new Order(items);

        // Assert
        Assert.Equal(6.50m, order.Subtotal);
        Assert.Equal(0.65m, order.Discount);  // 10% of 6.50
        Assert.Equal(5.85m, order.Total);
    }

    [Fact]
    public void Discount_SandwichOnly_ShouldBe0Percent()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(3, MenuItemName.XBacon, 7.00m, MenuItemType.Sandwich)
        };

        // Act
        var order = new Order(items);

        // Assert
        Assert.Equal(7.00m, order.Subtotal);
        Assert.Equal(0m, order.Discount);  // 0% discount
        Assert.Equal(7.00m, order.Total);
    }

    [Fact]
    public void Discount_SideOnly_ShouldBe0Percent()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(4, MenuItemName.Fries, 2.00m, MenuItemType.Side)
        };

        // Act
        var order = new Order(items);

        // Assert
        Assert.Equal(2.00m, order.Subtotal);
        Assert.Equal(0m, order.Discount);
        Assert.Equal(2.00m, order.Total);
    }

    [Fact]
    public void Discount_DrinkOnly_ShouldBe0Percent()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(5, MenuItemName.Soda, 2.50m, MenuItemType.Drink)
        };

        // Act
        var order = new Order(items);

        // Assert
        Assert.Equal(2.50m, order.Subtotal);
        Assert.Equal(0m, order.Discount);
        Assert.Equal(2.50m, order.Total);
    }

    [Fact]
    public void Discount_SideDrink_ShouldBe0Percent()
    {
        // Arrange
        var items = new List<OrderItem>
        {
            new(4, MenuItemName.Fries, 2.00m, MenuItemType.Side),
            new(5, MenuItemName.Soda, 2.50m, MenuItemType.Drink)
        };

        // Act
        var order = new Order(items);

        // Assert
        Assert.Equal(4.50m, order.Subtotal);
        Assert.Equal(0m, order.Discount);
        Assert.Equal(4.50m, order.Total);
    }

    [Fact]
    public void Discount_DifferentSandwichCombinations_ShouldCalculateCorrectly()
    {
        // XBacon + Fries + Soda = 7.00 + 2.00 + 2.50 = 11.50, 20% discount
        var items = new List<OrderItem>
        {
            new(3, MenuItemName.XBacon, 7.00m, MenuItemType.Sandwich),
            new(4, MenuItemName.Fries, 2.00m, MenuItemType.Side),
            new(5, MenuItemName.Soda, 2.50m, MenuItemType.Drink)
        };

        var order = new Order(items);

        Assert.Equal(11.50m, order.Subtotal);
        Assert.Equal(2.30m, order.Discount);  // 20% of 11.50
        Assert.Equal(9.20m, order.Total);
    }
}