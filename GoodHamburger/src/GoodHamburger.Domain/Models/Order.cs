using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Models;

public class Order
{
    private readonly List<OrderItem> _items = new();
    
    public int Id { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public decimal Subtotal { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Total { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Order() { }

    public Order(List<OrderItem> items)
    {
        if (items == null || items.Count == 0)
            throw new InvalidOperationException("Order must have at least one item.");

        ValidateNoDuplicates(items);
        ValidateMaxLimits(items);

        _items = items;
        CreatedAt = DateTime.UtcNow;
        RecalculateTotal();
    }

    public void UpdateItems(List<OrderItem> newItems)
    {
        if (newItems == null || newItems.Count == 0)
            throw new InvalidOperationException("Order must have at least one item.");

        ValidateNoDuplicates(newItems);
        ValidateMaxLimits(newItems);

        _items.Clear();
        _items.AddRange(newItems);
        RecalculateTotal();
    }

    private void ValidateNoDuplicates(List<OrderItem> items)
    {
        var groupedItems = items.GroupBy(i => i.MenuItemId);
        foreach (var group in groupedItems)
        {
            if (group.Count() > 1)
                throw new InvalidOperationException($"Duplicate item detected: {group.First().MenuItemName}. Each item can only be added once.");
        }
    }

    private void ValidateMaxLimits(List<OrderItem> items)
    {
        var sandwichCount = items.Count(i => i.Type == MenuItemType.Sandwich);
        var sideCount = items.Count(i => i.Type == MenuItemType.Side);
        var drinkCount = items.Count(i => i.Type == MenuItemType.Drink);

        if (sandwichCount > 1)
            throw new InvalidOperationException("Maximum 1 sandwich allowed per order.");
        if (sideCount > 1)
            throw new InvalidOperationException("Maximum 1 side allowed per order.");
        if (drinkCount > 1)
            throw new InvalidOperationException("Maximum 1 drink allowed per order.");
    }

    private void RecalculateTotal()
    {
        Subtotal = _items.Sum(i => i.Price);

        var hasSandwich = _items.Any(i => i.Type == MenuItemType.Sandwich);
        var hasSide = _items.Any(i => i.Type == MenuItemType.Side);
        var hasDrink = _items.Any(i => i.Type == MenuItemType.Drink);

        decimal discountPercentage = 0;

        if (hasSandwich && hasSide && hasDrink)
            discountPercentage = 0.20m;
        else if (hasSandwich && hasDrink)
            discountPercentage = 0.15m;
        else if (hasSandwich && hasSide)
            discountPercentage = 0.10m;

        Discount = Subtotal * discountPercentage;
        Total = Subtotal - Discount;
    }
}