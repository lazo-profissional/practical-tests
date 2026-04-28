using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Models;
using Xunit;

namespace GoodHamburger.Tests.Domain;

public class MenuItemTests
{
    [Fact]
    public void GetFullMenu_ShouldReturnAllFiveItems()
    {
        // Act
        var menu = MenuItem.GetFullMenu();

        // Assert
        Assert.Equal(5, menu.Count);
    }

    [Fact]
    public void GetFullMenu_ShouldContainThreeSandwiches()
    {
        // Act
        var menu = MenuItem.GetFullMenu();

        // Assert
        Assert.Equal(3, menu.Count(m => m.Type == MenuItemType.Sandwich));
    }

    [Fact]
    public void GetFullMenu_ShouldContainOneSide()
    {
        // Act
        var menu = MenuItem.GetFullMenu();

        // Assert
        Assert.Single(menu, m => m.Type == MenuItemType.Side);
    }

    [Fact]
    public void GetFullMenu_ShouldContainOneDrink()
    {
        // Act
        var menu = MenuItem.GetFullMenu();

        // Assert
        Assert.Single(menu, m => m.Type == MenuItemType.Drink);
    }

    [Fact]
    public void GetFullMenu_ShouldHaveCorrectPrices()
    {
        // Act
        var menu = MenuItem.GetFullMenu();

        // Assert
        Assert.Equal(5.00m, menu.First(m => m.Name == MenuItemName.XBurger).Price);
        Assert.Equal(4.50m, menu.First(m => m.Name == MenuItemName.XEgg).Price);
        Assert.Equal(7.00m, menu.First(m => m.Name == MenuItemName.XBacon).Price);
        Assert.Equal(2.00m, menu.First(m => m.Name == MenuItemName.Fries).Price);
        Assert.Equal(2.50m, menu.First(m => m.Name == MenuItemName.Soda).Price);
    }
}